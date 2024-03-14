using UnityEngine;

namespace TankMaster.Gameplay
{
    public interface IDamageable
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public Health Health { get; }

        public void ApplyDamage(int damage) => 
            Health.ApplyDamage(damage);
    }
}