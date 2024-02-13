using UnityEngine;

namespace TankMaster.UI
{
    public class RotateToCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            if(_camera == null)
                _camera = Camera.main;
        }

        private void Reset() => 
            _camera = Camera.main;

        private void LateUpdate() => 
            transform.LookAt(_camera.transform);
    }
}
