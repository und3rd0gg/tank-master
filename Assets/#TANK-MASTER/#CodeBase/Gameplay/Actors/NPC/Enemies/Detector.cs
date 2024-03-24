using System.Collections.Generic;
using TankMaster.AIPerception;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies
{
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

        public Transform GetClosestEnemy()
        {
            Transform closestEnemy = null;
            var minDist = Mathf.Infinity;
            var currentPos = transform.position;
            
            foreach (var t in _detectedObjects)
            {
                var dist = Vector3.Distance(t.transform.position, currentPos);
                
                if (dist < minDist)
                {
                    closestEnemy = t.transform;
                    minDist = dist;
                }
            }
            
            return closestEnemy;
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