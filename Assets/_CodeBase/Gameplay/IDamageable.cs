using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    public interface IDamageable
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public Health Health { get; }

        public void ApplyDamage(uint damage) => 
            Health.ApplyDamage(damage);
    }
}