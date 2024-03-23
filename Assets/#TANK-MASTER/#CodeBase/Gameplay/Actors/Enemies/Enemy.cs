namespace TankMaster.Gameplay.Actors.Enemies
{
  public class Enemy : EnemyNPCBase
  {
#if UNITY_EDITOR
    private void OnDrawGizmos() {
      NpcProfile.VisionZoneSettings.TryDrawGizmos(Pivot);
      NpcProfile.ChaseSettings.TryDrawGizmos(transform);
    }
#endif
  }
}