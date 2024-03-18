using UnityEngine;

namespace TankMaster.UI.HUD
{
    public class EnemyPointer : MonoBehaviour
    {
        private OffscreenPointer _offscreenPointer;
        private Transform _enemy;

        public EnemyPointer Init(Canvas canvas, Camera mainCamera, Transform playerTransform, Transform enemy,
            Vector2 pointerSize) {
            _enemy = enemy;
            _offscreenPointer = new OffscreenPointer(canvas, mainCamera, playerTransform,
                (RectTransform)transform, pointerSize);
            return this;
        }

        private void LateUpdate() {
            _offscreenPointer.UpdatePointer(_enemy.position);
        }
    }
}