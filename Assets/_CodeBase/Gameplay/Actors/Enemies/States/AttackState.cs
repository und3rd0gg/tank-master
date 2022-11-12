using TankMaster._CodeBase.StaticData;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies.States
{
    public class AttackState : IPayloadedState<Transform>
    {
        private readonly ActorStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly EnemyProfile _enemyProfile;
        private readonly IAttacker _attacker;
        private readonly Mover _mover;
        private readonly Detector _detector;

        private Transform _target;

        public AttackState(EnemyStateMachine enemyStateMachine, EnemyAnimator enemyAnimator, EnemyProfile enemyProfile,
            IAttacker attacker, Mover mover, Detector detector)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyAnimator = enemyAnimator;
            _enemyProfile = enemyProfile;
            _attacker = attacker;
            _mover = mover;
            _detector = detector;
        }

        public void Enter(Transform payload)
        {
            _target = payload;
            InitializeAttacker();
            _enemyAnimator.SetAttack(true);

            void InitializeAttacker()
            {
                _attacker.SetTarget(payload);
                _attacker.enabled = true;
            }
        }

        public void Tick()
        {
            _mover.RotateToTarget(_target);
            
            if (!_attacker.IsInEffectiveDistance())
            {
                _enemyStateMachine.Enter<ChaseState, Transform>(_target);
                return;
            }
        }

        public void Exit()
        {
            _enemyAnimator.SetAttack(false);
            _attacker.enabled = false;
        }
    }
}