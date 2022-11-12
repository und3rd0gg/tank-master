using System;
using System.Collections.Generic;
using TankMaster._CodeBase.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    [Serializable]
    public class AttackProfile
    {
        [field: SerializeField] public float EffectiveDistance { get; private set; }
        public List<ProjectileInfo> ProjectileInfo;
    }

    [Serializable]
    public struct ProjectileInfo
    {
        [field: SerializeField] [Tooltip("Projectile prefab")] public Projectile Projectile { get; private set; }
        [field: SerializeField] [Tooltip("Delay between shots")] public float Delay { get; private set; }
    }
}