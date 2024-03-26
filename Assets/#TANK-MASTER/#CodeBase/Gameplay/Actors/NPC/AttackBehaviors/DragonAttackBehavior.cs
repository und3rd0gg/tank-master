using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.AttackBehaviors
{
  public class DragonAttackBehavior : AttackBehaviorBase
  {
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ProjectileBase _projectile;
    
    public override void Attack(Vector3 target) {
      var shootPoint = _shootPoint.position;
      var proj = Instantiate(_projectile, shootPoint, Quaternion.identity);
      proj.Launch(shootPoint, target);
    }
  }
}