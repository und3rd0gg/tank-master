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
        private readonly EnemyProfile _enemyProfile;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Shooter _shooter;
        private readonly Detector _detector;
        private readonly CancellationTokenSource _chaseCooldownCancellationToken = new();
        private IDamageable _target;

        public ChaseAndShoot(StateMachine stateMachine, EnemyProfile enemyProfile, NavMeshAgent navMeshAgent,
            Shooter shooter, Detector detector)
        {
            _stateMachine = stateMachine;
            _enemyProfile = enemyProfile;
            _navMeshAgent = navMeshAgent;
            _shooter = shooter;
            _detector = detector;
        }

        public void Enter(IDamageable payload)
        {
            _target = payload;
            InitializeShooter();
            _navMeshAgent.isStopped = false;
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
            await UniTask.Delay(TimeSpan.FromSeconds(_enemyProfile.ChaseCooldown),
                cancellationToken: cancellationToken);
            _stateMachine.Enter<Idle>();
        }

        private bool PlayerNotReached() =>
            Vector3.Distance(_navMeshAgent.transform.position, _target.transform.position) >=
            _navMeshAgent.stoppingDistance;
    }
}