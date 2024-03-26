namespace TankMaster.Gameplay.Actors.NPC.DeathBehaviors
{
  public sealed class DefaultDeathBehavior : DeathBehaviorBase
  {
    public override void OnDeath(Health health) {
      Destroy(gameObject);
    }
  }
}