using System;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [Serializable]
    public class OverlapSettings
    {
        public float Radius;
        public Transform OverlapPoint;
        public LayerMask EnemyMask;

#if UNITY_EDITOR
        public Color GizmoColor;
#endif

#if UNITY_EDITOR
        public void TryDrawGizmos() {
            Gizmos.color = GizmoColor;
            Gizmos.DrawWireSphere(OverlapPoint.position, Radius);
        }
#endif
    }
}