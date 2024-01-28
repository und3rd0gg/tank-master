using UnityEngine;
using UnityEngine.AI;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;

        [SerializeField]private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        private void Update()
        {
            if (ShouldMove())
                _animator.SetRun(true);
            else
                _animator.SetRun(false);
        }

        private bool ShouldMove() =>
            _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}