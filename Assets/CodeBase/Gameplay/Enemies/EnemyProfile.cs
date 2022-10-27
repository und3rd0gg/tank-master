using BuildingBlocks.DataTypes;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "New Enemy Profile", menuName = "Gameplay/Enemy Profile", order = 0)]
    public class EnemyProfile : ScriptableObject
    {
        [SerializeField] private InspectableDictionary<Projectile, int> _projectiles;
    }
}