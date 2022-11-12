using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies.States
{
    public class IdleState : IDefaultState
    {
        private readonly ActorStateMachine _enemyStateMachine;
        private readonly Detector _detector;

        public IdleState(ActorStateMachine enemyStateMachine, Detector detector)
        {
            _enemyStateMachine = enemyStateMachine;
            _detector = detector;
        }

        public void Enter()
        {
            _detector.ObjectDetected += OnObjectDetected;
        }

        public void Exit()
        {
            _detector.ObjectDetected -= OnObjectDetected;
        }

        public void Tick()
        { }

        private void OnObjectDetected(GameObject source, GameObject detectedObject)
        {
            _enemyStateMachine.Enter<ChaseState, Transform>(detectedObject.transform);
        }
    }
}