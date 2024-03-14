using TankMaster.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace TankMaster.Logic
{
    public class EnemySpawner : MonoBehaviour
    {
        [FormerlySerializedAs("EnemyTypeId")] public EnemyType _enemyType;
    }
}