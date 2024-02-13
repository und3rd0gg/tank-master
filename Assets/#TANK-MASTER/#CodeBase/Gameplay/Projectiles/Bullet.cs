using System.Collections.Generic;
using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
    public class Bullet : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _launchForce;

        public override void Launch(Vector3 startPosition, Vector3 target)
        {
            transform.LookAt(target);
            _rigidbody.AddForce(transform.forward * _launchForce, ForceMode.VelocityChange);
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