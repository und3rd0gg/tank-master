using System;
using System.Collections.Generic;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "New Enemy Profile", menuName = "Gameplay/Enemy Profile", order = 0)]
    public class ShootProfile : ScriptableObject
    {
        public List<ProjectileInfo> ProjectileInfo;
    }

    [Serializable]
    public struct ProjectileInfo
    {
        [field: SerializeField] [Tooltip("Projectile prefab")] public Projectile Projectile { get; private set; }
        [field: SerializeField] [Tooltip("Delay between shots")] public float Delay { get; private set; }
    }
}