using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
  [CreateAssetMenu(fileName = nameof(EnemyProfile), menuName = "Gameplay/" + nameof(EnemyProfile))]
  public class EnemyProfile : ScriptableObject
  {
    [field: SerializeField] public OverlapSettings VisionZoneSettings { get; private set; }
    [field: SerializeField] public ChaseSettings ChaseSettings { get; private set; }
    [field: SerializeField] public PatrolSettings PatrolSettings { get; private set; }

    [field: SerializeField] public float DistanceThreshold { get; private set; }
  }
}