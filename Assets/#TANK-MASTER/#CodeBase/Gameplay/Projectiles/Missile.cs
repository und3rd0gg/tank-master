using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
  public class Missile : ProjectileBase
  {
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _flyTime = 2.5f;

    private void FixedUpdate() {
      var velocity = _rigidbody.velocity;
      float detectionDistance = velocity.magnitude * Time.deltaTime;
      Vector3 direction = velocity;
      
      if (_rigidbody.useGravity) {
        direction += Physics.gravity * Time.deltaTime;
      }

      direction = direction.normalized;

      if (Physics.SphereCast(transform.position, ImpactRadius, direction, out var hit,
            detectionDistance))
      {
        Debug.Log("col");
        DoImpact();
      }
    }

    public override void Launch(Vector3 startPosition, Vector3 target) {
      var force = Blobcreate.ProjectileToolkit.Projectile.VelocityByTime(startPosition, target,
        _flyTime);
      _rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
  }
}