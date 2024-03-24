﻿using System;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies
{
    [Serializable]
    public struct ProjectileInfo
    {
        [field: SerializeField] [Tooltip("Projectile prefab")] public Projectile Projectile { get; private set; }
        [field: SerializeField] [Tooltip("Delay between shots")] public float Delay { get; private set; }
    }
}