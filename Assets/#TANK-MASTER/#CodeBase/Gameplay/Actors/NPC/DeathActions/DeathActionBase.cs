using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.DeathActions
{
  public abstract class DeathActionBase : MonoBehaviour, IDeathAction
  {
    public abstract void OnDeath(Health health);
  }

  public interface IDeathAction
  {
    public void OnDeath(Health health);
  }
}