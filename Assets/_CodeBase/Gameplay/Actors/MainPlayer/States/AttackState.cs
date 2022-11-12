using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Gameplay.Actors.Enemies.States;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer.States
{
    public class AttackState : IDefaultState
    {
        private readonly ActorStateMachine _stateMachine;
        private readonly Shooter _shooter;
        private readonly Detector _detector;

        public AttackState(ActorStateMachine stateMachine, Shooter shooter, Detector detector)
        {
            _stateMachine = stateMachine;
            _shooter = shooter;
            _detector = detector;
        }

        public void Enter()
        {
            SetNearestDetectedObjectAsTarget();
            _shooter.enabled = true;
        }

        public void Tick()
        {
            if (_detector.DetectedObjects.Count < 1)
            {
                _stateMachine.Enter<IdleState>();
                return;
            }
            
            SetNearestDetectedObjectAsTarget();
        }

        public void Exit()
        {
            _shooter.enabled = false;
        }

        private void SetNearestDetectedObjectAsTarget() => 
            _shooter.SetTarget(_detector.GetClosestEnemy());
    }
}