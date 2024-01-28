using TankMaster._CodeBase.Logic;
using UnityEditor;
using UnityEngine;

namespace TankMaster._CodeBase.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        private const float GizmoRadius = 0.5f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner enemySpawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(enemySpawner.transform.position, GizmoRadius);
        }
    }
}