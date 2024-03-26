using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Animators
{
    public class NPCAnimatorProvider : AnimatorProviderBase
    {
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");

        public void SetDeath() {
            Animator.SetTrigger(_dieHash);
        }
        
        public void SetMoveSpeed(float speed) {
            Animator.SetFloat(_moveSpeedHash, speed);
        }
    }
}