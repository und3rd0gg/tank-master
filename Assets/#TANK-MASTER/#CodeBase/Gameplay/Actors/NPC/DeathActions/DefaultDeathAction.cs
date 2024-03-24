namespace TankMaster.Gameplay.Actors.NPC.DeathActions
{
  public sealed class DefaultDeathAction : DeathActionBase {
    public override void OnDeath(Health health) {
      Destroy(gameObject);
    }
  }
}