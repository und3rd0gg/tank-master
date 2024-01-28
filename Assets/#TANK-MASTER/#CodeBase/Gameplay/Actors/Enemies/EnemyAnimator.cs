using System;

using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private readonly int Die = Animator.StringToHash(nameof(Die));
        private readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
        private readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));

        public event Action Attacked;

        [SerializeField] protected Animator Animator;

        private void OnAttack() =>
            Attacked?.Invoke();

        public void PlayDeath() =>
            Animator.SetTrigger(Die);

        public void SetRun(bool isRunning) =>
            Animator.SetBool(IsRunning, isRunning);

        public void SetAttack(bool isAttacking) =>
            Animator.SetBool(IsAttacking, isAttacking);
    }
}