using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField][Attach] private Animator _animator;

        public void PlayDeath() =>
            _animator.SetTrigger(Die);
    }
}
