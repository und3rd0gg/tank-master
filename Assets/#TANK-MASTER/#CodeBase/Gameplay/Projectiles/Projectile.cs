using System.Collections.Generic;
using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float ImpactRadius = 0.5f;
        [SerializeField] protected int Damage;

        public abstract void Launch(Vector3 startPosition, Vector3 target);

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