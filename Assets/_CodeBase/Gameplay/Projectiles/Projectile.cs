using System.Collections.Generic;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float ImpactRadius = 0.5f;
        [SerializeField] protected uint Damage;

        public abstract void Launch(Vector3 startPosition, Transform target);

        public void DoImpact()
        {
            var damageables = GetDamageables();
            
            foreach (var damageable in damageables)
            {
                damageable.ApplyDamage(Damage);
            }
        }

        protected abstract List<IDamageable> GetDamageables();
    }
}