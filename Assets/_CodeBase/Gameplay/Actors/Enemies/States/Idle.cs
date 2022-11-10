using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies.States
{
    public class Idle : IDefaultState
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly Detector _detector;

        public Idle(EnemyStateMachine enemyStateMachine, Detector detector)
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
            _enemyStateMachine.Enter<Chase, Transform>(detectedObject.transform);
        }
    }
}