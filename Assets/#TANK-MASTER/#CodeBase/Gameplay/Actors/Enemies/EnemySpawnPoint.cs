using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
  public class EnemySpawnPoint : MonoBehaviour
  {
    [field: SerializeField] public NPCType NpcType { get; private set; }
  }

  public enum NPCType
  {
    Kamikaze,
    Soldier,
    Dragon,
    Random,
  }
}