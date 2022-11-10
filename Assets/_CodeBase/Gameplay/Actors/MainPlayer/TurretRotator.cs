using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class TurretRotator : MonoBehaviour
    {
        [SerializeField] private Detector _detector;

        private Quaternion _defaultRotation;

        private void Awake()
        {
            _defaultRotation = transform.rotation;
        }

        private void Update()
        {
            var closestObject = GetClosestObject();

            if (closestObject == null)
            {
                RotateToDefault();
            }
            else
            {
                transform.LookAt(closestObject);
            }
        }

        private void RotateToDefault()
        {
            transform.rotation = _defaultRotation;
        }

        private Transform GetClosestObject()
        {
            Transform _closestObject;
            var detectedObjects = _detector.DetectedObjects;

            if (detectedObjects.Count < 1)
                return null;

            _closestObject = detectedObjects[0].transform;

            for (int i = 1; i < detectedObjects.Count; i++)
            {
                var distanceToClosestObject = Vector3.Distance(transform.position, _closestObject.position);
                var distanceToCurrentObject =
                    Vector3.Distance(transform.position, detectedObjects[i].transform.position);

                if (distanceToCurrentObject < distanceToClosestObject)
                    _closestObject = detectedObjects[i].transform;
            }

            return _closestObject;
        }
    }
}