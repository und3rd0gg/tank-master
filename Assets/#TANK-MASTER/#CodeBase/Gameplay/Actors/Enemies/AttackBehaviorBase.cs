using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
  public abstract class AttackBehaviorBase : MonoBehaviour
  {
    public abstract void Attack(DamageableBase target);
  }
}