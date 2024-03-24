using System;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies.Settings
{
    [Serializable]
    public class OverlapSettings
    {
        public float Radius;
        public LayerMask EnemyMask;

#if UNITY_EDITOR
        public Color GizmoColor;
#endif

#if UNITY_EDITOR
        public void TryDrawGizmos(Transform overlapPoint) {
            Gizmos.color = GizmoColor;
            Gizmos.DrawWireSphere(overlapPoint.position, Radius);
        }
#endif
    }
}