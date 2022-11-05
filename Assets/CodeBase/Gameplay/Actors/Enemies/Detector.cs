using System.Collections.Generic;
using TankMaster.AIPerception;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Detector : MonoBehaviour, IDetector
    {
        private List<GameObject> _detectedObjects = new();

        public LayerMask _detectables { get; }

        public event ObjectDetectionHandler ObjectDetected;
        public event ObjectDetectionHandler DetectionReleased;

        public IReadOnlyList<GameObject> DetectedObjects => _detectedObjects;

        public void Detect(IDetectableObject detectedObject)
        {
            if (!_detectedObjects.Contains(detectedObject.gameObject))
            {
                detectedObject.Detect(gameObject);
                _detectedObjects.Add(detectedObject.gameObject);
                ObjectDetected?.Invoke(gameObject, detectedObject.gameObject);
            }
        }

        public void Detect(GameObject detectedObject)
        {
            if (!_detectedObjects.Contains(detectedObject))
            {
                _detectedObjects.Add(detectedObject.gameObject);
                ObjectDetected?.Invoke(gameObject, detectedObject.gameObject);
            }
        }

        public void ReleaseDetection(IDetectableObject detectedObject)
        {
            if (_detectedObjects.Contains(detectedObject.gameObject))
            {
                detectedObject.ReleaseDetection(gameObject);
                _detectedObjects.Remove(detectedObject.gameObject);
                DetectionReleased?.Invoke(gameObject, detectedObject.gameObject);
            }
        }

        public void ReleaseDetection(GameObject detectedObject)
        {
            if (_detectedObjects.Contains(detectedObject))
            {
                _detectedObjects.Remove(detectedObject.gameObject);
                DetectionReleased?.Invoke(gameObject, detectedObject.gameObject);
            }
        }

        public Transform GetClosestDetectedObject()
        {
            Transform _closestObject;

            if (DetectedObjects.Count < 1)
                return null;

            _closestObject = DetectedObjects[0].transform;

            for (int i = 1; i < DetectedObjects.Count; i++)
            {
                var distanceToClosestObject = Vector3.Distance(transform.position, _closestObject.position);
                var distanceToCurrentObject =
                    Vector3.Distance(transform.position, DetectedObjects[i].transform.position);

                if (distanceToCurrentObject < distanceToClosestObject)
                    _closestObject = DetectedObjects[i].transform;
            }

            return _closestObject;
        }

        public GameObject GetClosestDetectedObejct() => 
            GetClosestDetectedObejct().gameObject;

        private void OnTriggerEnter(Collider other)
        {
            if (IsColliderDetectableObject(other, out var detectedObject))
                Detect(detectedObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsColliderDetectableObject(other, out var detectedObject))
                ReleaseDetection(detectedObject);
        }

        private bool IsColliderDetectableObject(Collider collider, out IDetectableObject detectedObject) =>
            collider.TryGetComponent(out detectedObject);
    }
}