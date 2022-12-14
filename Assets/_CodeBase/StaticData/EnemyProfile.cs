using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster._CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "New Enemy Profile", menuName = "Gameplay/Enemy Profile", order = 0)]
    public class EnemyProfile : ScriptableObject
    {
        [field: SerializeField] public float ChaseCooldown { get; private set; } = 3f;
        [field: SerializeField] public AttackProfile AttackProfile { get; private set; }
    }
}