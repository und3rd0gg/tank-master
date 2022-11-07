using TankMaster.StaticData;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies.States
{
    public class Attack : IPayloadedState<Transform>
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly EnemyProfile _enemyProfile;
        private readonly Shooter _shooter;
        private readonly Mover _mover;
        private readonly Detector _detector;

        private Transform _target;

        public Attack(EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator, EnemyProfile enemyProfile,
            Shooter shooter, Mover mover, Detector detector)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyAnimator = enemyAnimator;
            _enemyProfile = enemyProfile;
            _shooter = shooter;
            _mover = mover;
            _detector = detector;
        }

        public void Enter(Transform payload)
        {
            _target = payload;
            InitializeShooter();
            _enemyAnimator.SetAttack(true);

            void InitializeShooter()
            {
                _shooter.SetTarget(payload);
                _shooter.enabled = true;
            }
        }

        public void Tick()
        {
            _mover.RotateToTarget(_target);
            
            if (_mover.TargetNotReached())
            {
                _enemyStateMachine.Enter<Chase, Transform>(_target);
                return;
            }
        }

        public void Exit()
        {
            _enemyAnimator.SetAttack(false);
            _shooter.enabled = false;
        }
    }
}