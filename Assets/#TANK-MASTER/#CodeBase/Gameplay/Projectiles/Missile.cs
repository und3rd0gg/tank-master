using System.Collections.Generic;

using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Projectiles
{
    public class Missile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _flyTime = 2.5f;

        public override void Launch(Vector3 startPosition, Vector3 target)
        {
            var force = Blobcreate.ProjectileToolkit.Projectile.VelocityByTime(startPosition, target,
                _flyTime);
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        protected override List<IDamageable> GetDamageables()
        {
            var impactedObjects = Physics.OverlapSphere(transform.position, ImpactRadius);
            var damageables = new List<IDamageable>(impactedObjects.Length);

            foreach (var impactedObject in impactedObjects)
            {
                if (impactedObject.TryGetComponent(out IDamageable damageable))
                {
                    damageables.Add(damageable);
                }
            }

            return damageables;
        }
    }
}