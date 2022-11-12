using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Gameplay.Actors.Enemies.States;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer.States
{
    public class IdleState : IDefaultState
    {
        private readonly ActorStateMachine _stateMachine;
        private readonly Detector _detector;
        private readonly TurretRotator _turretRotator;

        public IdleState(ActorStateMachine stateMachine, Detector detector, TurretRotator turretRotator)
        {
            _stateMachine = stateMachine;
            _detector = detector;
            _turretRotator = turretRotator;
        }

        public void Enter()
        {
            _detector.ObjectDetected += DetectorOnObjectDetected;
        }

        private void DetectorOnObjectDetected(GameObject source, GameObject detectedObject)
        {
            _stateMachine.Enter<AttackState>();
        }

        public void Tick() { }

        public void Exit()
        {
            _detector.ObjectDetected -= DetectorOnObjectDetected;
        }
    }
}