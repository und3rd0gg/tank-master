using UnityEngine;
using UnityEngine.UI;

namespace TankMaster.UI.HUD
{
    public class EnemyPointer : MonoBehaviour
    {
        [SerializeField] private Image _pointerImage;
        
        private OffscreenPointer _offscreenPointer;
        private Transform _enemy;

        public EnemyPointer Init(Canvas canvas, Camera mainCamera, Transform playerTransform, Transform enemy,
            Vector2 pointerSize) {
            _enemy = enemy;
            _offscreenPointer = new OffscreenPointer(canvas, mainCamera, playerTransform,
                (RectTransform)transform, pointerSize, this);
            return this;
        }

        private void LateUpdate() {
            _offscreenPointer.UpdatePointer(_enemy.position);
        }
        
        public void Show() {
            _pointerImage.enabled = true;
        }

        public void Hide() {
            _pointerImage.enabled = false;
        }
    }
}