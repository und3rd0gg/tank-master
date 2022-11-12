using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TankMaster._CodeBase.StaticData;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies.States
{
    public class ChaseState : IPayloadedState<Transform>
    {
        private readonly ActorStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly EnemyProfile _enemyProfile;
        private readonly Mover _mover;
        private readonly Detector _detector;
        private readonly CancellationTokenSource _chaseCooldownCancellationToken = new();

        private Transform _target;

        public ChaseState(ActorStateMachine enemyStateMachine, EnemyAnimator enemyAnimator, EnemyProfile enemyProfile,
            Mover mover, Detector detector)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyAnimator = enemyAnimator;
            _enemyProfile = enemyProfile;
            _mover = mover;
            _detector = detector;
        }

        public void Enter(Transform payload)
        {
            _target = payload;
            _mover.SetTarget(_target);
            _enemyAnimator.SetRun(true);
            _detector.ObjectDetected += OnObjectDetected;
            _detector.DetectionReleased += OnDetectionReleased;
        }

        public void Tick()
        {
            if (_mover.TargetNotReached())
            {
                _mover.MoveTo(_target);
            }
            else
            {
                _enemyStateMachine.Enter<AttackState, Transform>(_target);
            }
        }

        public void Exit()
        {
            _mover.Stop();
            _enemyAnimator.SetRun(false);
            _detector.ObjectDetected -= OnObjectDetected;
            _detector.DetectionReleased -= OnDetectionReleased;
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
    }
}