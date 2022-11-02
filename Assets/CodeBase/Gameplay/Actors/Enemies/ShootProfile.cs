using System;
using System.Collections.Generic;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [Serializable]
    public class ShootProfile
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