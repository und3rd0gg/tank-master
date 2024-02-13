using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class TurretRotator : MonoBehaviour
    {
        private Quaternion _defaultRotation;
        private Transform _currentTarget;

        private void Awake()
        {
            _defaultRotation = transform.rotation;
        }

        private void Update()
        {
            LookAtClosestObject();
        }

        public void EnableRotation(Transform newTarget)
        {
            _currentTarget = newTarget;
            enabled = true;
        }

        public void DisableRotation() => 
            enabled = false;

        private void LookAtClosestObject() => 
            LookAtOneAxis(_currentTarget);

        private void RotateToDefault()
        {
            transform.rotation = _defaultRotation;
        }

        private void LookAtOneAxis(Transform target)
        {
            var lookPosition = new Vector3(target.transform.position.x, transform.position.y,
                target.transform.position.z);
            transform.LookAt(lookPosition);
        }
    }
}