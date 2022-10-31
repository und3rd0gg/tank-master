using UnityEngine;

namespace TankMaster.Gameplay
{
    public interface IDamageable
    {
        public Transform transform { get; }
        
        public uint Health { get; }
        
        public void ApplyDamage(uint damage);
    }
}