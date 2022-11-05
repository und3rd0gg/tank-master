using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Gameplay.Actors.Enemies.States;

namespace TankMaster.Gameplay.Actors.MainPlayer.States
{
    public class Attack : IDefaultState
    {
        private readonly ActorStateMachine _stateMachine;
        private readonly Shooter _shooter;
        private readonly Detector _detector;

        public Attack(ActorStateMachine stateMachine, Shooter shooter, Detector detector)
        {
            _stateMachine = stateMachine;
            _shooter = shooter;
            _detector = detector;
        }

        public void Enter()
        {
            _shooter.SetTarget(_detector.DetectedObjects[0].GetComponent<IDamageable>());
            _shooter.enabled = true;
        }

        public void Tick()
        {
            //_shooter.SetTarget(_detector.DetectedObjects[0].GetComponent<IDamageable>());
        }

        public void Exit()
        {
            _shooter.enabled = false;
        }
    }
}