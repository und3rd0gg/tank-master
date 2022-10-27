using UnityEngine;

namespace TankMaster.Gameplay.MainPlayer
{
    public class Player : MonoBehaviour, IActor, IDamageable
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        
        public void ApplyDamage(uint damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
