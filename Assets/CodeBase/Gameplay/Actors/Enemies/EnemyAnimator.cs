using System;
using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private readonly int Die = Animator.StringToHash(nameof(Die));
        private readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
        private readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));

        public event Action Attacked;

        [SerializeField] [Attach] private Animator _animator;

        private void OnAttack() =>
            Attacked?.Invoke();

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void SetRun(bool isRunning) =>
            _animator.SetBool(IsRunning, isRunning);

        public void SetAttack(bool isAttacking) =>
            _animator.SetBool(IsAttacking, isAttacking);
    }
}