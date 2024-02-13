using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public class PlayerFollower : Follower
    {
        private Transform _player;

        private void Update()
        {
            NavMeshAgent.destination = _player.position;
        }

        private void OnEnable()
        {
            NavMeshAgent.isStopped = false;
        }

        private void OnDisable()
        {
            NavMeshAgent.isStopped = true;
        }

        public override void SetTarget(Transform target) => 
            _player = target;
    }
}