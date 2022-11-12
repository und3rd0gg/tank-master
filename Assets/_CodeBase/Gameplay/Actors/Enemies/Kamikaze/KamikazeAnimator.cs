using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies.Kamikaze
{
    public class KamikazeAnimator : EnemyAnimator
    {
        private readonly int SelfDestruct = Animator.StringToHash(nameof(SelfDestruct));
        
        public void SetSelfDestruction() =>
            Animator.SetTrigger(SelfDestruct);
    }
}