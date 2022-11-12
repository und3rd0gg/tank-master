using UnityEngine;

namespace TankMaster._CodeBase.UI
{
    public class RotateToCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Awake()
        {
            if(_camera == null)
                _camera = Camera.main;
        }

        private void Reset()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
    }
}
