using System.Collections.Generic;
using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private float _impactRadius = 0.5f;

        public abstract void Launch(Vector3 startPosition, Vector3 target);

        public virtual void DoImpact()
        {
            var impactedObjects = Physics.OverlapSphere(transform.position, _impactRadius);
            var damageables = new List<IDamageable>(impactedObjects.Length);

            foreach (var impactedObject in impactedObjects)
            {
                var damageable = impactedObject.GetComponentInParent<IDamageable>();

                if (damageable != null)
                {
                    damageables.Add(damageable);
                    Debug.Log(impactedObject.name);
                }
            }
        }
    }
}