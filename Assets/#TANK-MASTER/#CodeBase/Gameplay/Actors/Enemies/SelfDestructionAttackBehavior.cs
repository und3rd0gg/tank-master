using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
  public class SelfDestructionAttackBehavior : AttackBehaviorBase
  {
    public override void Attack(DamageableBase target) {
      Debug.Log("взрыв");      
    }
  }
}