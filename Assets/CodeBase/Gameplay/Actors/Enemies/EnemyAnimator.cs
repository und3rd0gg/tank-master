using System;
using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private readonly int Die = Animator.StringToHash(nameof(Die));
        private readonly int IsStopped = Animator.StringToHash(nameof(IsStopped));
        private readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));

        public event Action Attacked;

        [SerializeField] [Attach] private Animator _animator;

        private void OnAttack() =>
            Attacked?.Invoke();

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void PlayRun() =>
            _animator.SetBool(IsStopped, true);

        public void PlayAttack() =>
            _animator.SetBool(IsAttacking, true);
    }
}