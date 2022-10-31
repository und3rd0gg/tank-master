using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay.Enemies.States
{
    public class ChaseAndShoot : IPayloadedState<IDamageable>
    {
        private readonly StateMachine _stateMachine;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Shooter _shooter;
        private IDamageable _target;
        private const float StoppingDistance = 5f;
        private const float ChaseDelay = 2.5f;
        private Detector _detector;
        private CancellationTokenSource _chaseCooldownCancellationToken = new();

        public ChaseAndShoot(StateMachine stateMachine, NavMeshAgent navMeshAgent, Shooter shooter, Detector detector)
        {
            _stateMachine = stateMachine;
            _navMeshAgent = navMeshAgent;
            _shooter = shooter;
            _detector = detector;
        }

        public void Enter(IDamageable payload)
        {
            _target = payload;
            InitializeShooter();
            _detector.ObjectDetected += OnObjectDetected;
            _detector.DetectionReleased += OnDetectionReleased;

            void InitializeShooter()
            {
                _shooter.SetTarget(payload);
                _shooter.enabled = true;
            }
        }

        public void Tick()
        {
            if (PlayerNotReached())
            {
                _navMeshAgent.destination = _target.transform.position;
            }
        }

        public void Exit()
        {
            _navMeshAgent.isStopped = true;
            _detector.ObjectDetected -= OnObjectDetected;
            _detector.DetectionReleased -= OnDetectionReleased;
            _shooter.enabled = false;
        }

        private void OnObjectDetected(GameObject source, GameObject detectedObject)
        {
            if (detectedObject.gameObject == _target.gameObject)
            {
                _chaseCooldownCancellationToken.Cancel();
            }
        }

        private void OnDetectionReleased(GameObject source, GameObject detectedObject)
        {
            if (detectedObject.gameObject == _target.gameObject)
                ChaseCooldownAsync(_chaseCooldownCancellationToken.Token);
        }

        private async UniTask ChaseCooldownAsync(CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(ChaseDelay), cancellationToken: cancellationToken);
            _stateMachine.Enter<Idle>();
        }

        private bool PlayerNotReached() =>
            Vector3.Distance(_navMeshAgent.transform.position, _target.transform.position) >= StoppingDistance;
    }
}