using System.Collections.Generic;

using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Projectiles
{
    [RequireComponent(typeof(ETFXProjectileScript), typeof(SmartMissile3D))]
    public class HomingMissile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _launchForce;
        
        private Vector3 _target;

        public override void Launch(Vector3 startPosition, Vector3 target)
        {
            _target = target;
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