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

#if UNITY_EDITOR
        
        public void TryDrawGizmos(Transform center)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(center.position, ChaseStoppingDistance * ChaseStoppingDistance);
        }
        
#endif
    }
}