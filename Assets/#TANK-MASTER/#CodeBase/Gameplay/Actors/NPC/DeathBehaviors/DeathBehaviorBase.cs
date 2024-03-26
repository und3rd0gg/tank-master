using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.DeathBehaviors
{
  public interface IDeathAction
  {
    public void OnDeath(Health health);
  }

  public abstract class DeathBehaviorBase : MonoBehaviour, IDeathAction
  {
    public abstract void OnDeath(Health health);
  }
}