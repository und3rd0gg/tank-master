using System;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies.Settings
{
    [Serializable]
    public class ChaseSettings
    {
        [field: SerializeField] public float ChaseStoppingDistance { get; private set; }
        [field: SerializeField] public float TargetLostWaitTime { get; private set; }
        
        public float SqrChaseStoppingDistance => ChaseStoppingDistance * ChaseStoppingDistance;
    }
}