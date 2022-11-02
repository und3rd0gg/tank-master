using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies.States
{
    public class Idle : IDefaultState
    {
        private readonly StateMachine _stateMachine;
        private readonly Detector _detector;

        public Idle(StateMachine stateMachine, Detector detector)
        {
            _stateMachine = stateMachine;
            _detector = detector;
        }

        public void Exit()
        {
            _detector.ObjectDetected -= OnObjectDetected;
        }

        public void Enter()
        {
            _detector.ObjectDetected += OnObjectDetected;
        }

        private void OnObjectDetected(GameObject source, GameObject detectedObject)
        {
            _stateMachine.Enter<ChaseAndAttack, IDamageable>(detectedObject.GetComponent<IDamageable>());
        }

        public void Tick()
        { }
    }
}