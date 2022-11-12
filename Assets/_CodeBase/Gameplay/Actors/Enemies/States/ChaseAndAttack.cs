using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TankMaster._CodeBase.StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies.States
{
    public class ChaseAndAttack : IPayloadedState<Transform>
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyProfile _enemyProfile;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Shooter _shooter;
        private readonly Detector _detector;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly CancellationTokenSource _chaseCooldownCancellationToken = new();
        private Transform _target;

        public ChaseAndAttack(EnemyStateMachine enemyStateMachine, EnemyProfile enemyProfile, NavMeshAgent navMeshAgent,
            Shooter shooter, Detector detector, EnemyAnimator enemyAnimator)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyProfile = enemyProfile;
            _navMeshAgent = navMeshAgent;
            _shooter = shooter;
            _detector = detector;
            _enemyAnimator = enemyAnimator;
        }

        public void Enter(Transform payload)
        {
            _target = payload;
            InitializeShooter();
            _enemyAnimator.SetAttack(true);
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
            _enemyStateMachine.Enter<IdleState>();
        }

        private bool PlayerNotReached() =>
            Vector3.Distance(_navMeshAgent.transform.position, _target.transform.position) >=
            _navMeshAgent.stoppingDistance;
    }
}