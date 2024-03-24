using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies
{
    public class NPCAnimatorProvider : AnimatorProviderBase
    {
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _isAttackingHash = Animator.StringToHash("IsAttacking");
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");

        public void SetDeath() {
            Animator.SetTrigger(_dieHash);
        }

        public void SetAttack(bool isAttacking) {
            Animator.SetBool(_isAttackingHash, isAttacking);
        }
        
        public void SetMoveSpeed(float speed) {
            Animator.SetFloat(_moveSpeedHash, speed);
        }
    }
}