using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;

        [SerializeField]private NavMeshAgent _agent;
        [FormerlySerializedAs("_animator")] [SerializeField] private EnemyAnimatorProvider _animatorProvider;

        private void Update()
        {
            if (ShouldMove())
                _animatorProvider.SetRun(true);
            else
                _animatorProvider.SetRun(false);
        }

        private bool ShouldMove() =>
            _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}