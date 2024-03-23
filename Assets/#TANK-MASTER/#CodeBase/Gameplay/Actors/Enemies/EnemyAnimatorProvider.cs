using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public sealed class EnemyAnimatorProvider : AnimatorProviderBase
    {
        private readonly int DieHash = Animator.StringToHash("Die");
        private readonly int IsRunningHash = Animator.StringToHash("IsRunning");
        private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

        public void PlayDeath() {
            Animator.SetTrigger(DieHash);
        }

        public void SetRun(bool isRunning) {
            Animator.SetBool(IsRunningHash, isRunning);
        }

        public void SetAttack(bool isAttacking) {
            Animator.SetBool(IsAttackingHash, isAttacking);
        }
    }
}