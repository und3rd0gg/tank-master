using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
  public class EnemySpawnPoint : MonoBehaviour
  {
    [field: SerializeField] public EnemyType EnemyType { get; private set; }
  }

  public enum EnemyType
  {
    Kamikaze,
    Soldier,
    Dragon,
    Random,
  }
}