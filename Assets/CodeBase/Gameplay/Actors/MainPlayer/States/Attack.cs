using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Gameplay.Actors.Enemies.States;

namespace TankMaster.Gameplay.Actors.MainPlayer.States
{
    public class Attack : IDefaultState
    {
        private readonly ActorStateMachine _stateMachine;
        private readonly EnemyShooter _shooter;
        private readonly Detector _detector;

        public Attack(ActorStateMachine stateMachine, EnemyShooter shooter, Detector detector)
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
                _stateMachine.Enter<Idle>();
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