using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
    public class Missile : Projectile
    {
        [SerializeField] [Attach] private Rigidbody _rigidbody;
        [SerializeField] private float _flyTime = 2.5f;

        public override void Launch(Vector3 startPosition, Vector3 target)
        {
            var force = Blobcreate.ProjectileToolkit.Projectile.VelocityByTime(startPosition, target,
                _flyTime);
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
    }
}