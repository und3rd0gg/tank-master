using System.Collections.Generic;
using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected float ImpactRadius = 0.5f;
        [SerializeField] protected int Damage;

        public abstract void Launch(Vector3 startPosition, Vector3 target);

        protected virtual void DoImpact()
        {
            var damageables = GetDamageables();
            
            foreach (var damageable in damageables)
            {
                damageable.Health.ApplyDamage(Damage);
            }
        }

        protected virtual IEnumerable<DamageableBase> GetDamageables() {
            var impactedObjects = Physics.OverlapSphere(transform.position, ImpactRadius);
            var damageables = new List<DamageableBase>();

            for (var i = 0; i < impactedObjects.Length; i++) {
                damageables.AddRange(impactedObjects[i].GetComponentsInChildren<DamageableBase>());
            }

            return damageables;
        }
    }
}