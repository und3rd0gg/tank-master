using System.Collections.Generic;
using TankMaster._CodeBase.AIPerception;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    public class DetectableObject : MonoBehaviour, IDetectableObject
    {
        public event ObjectDetectionHandler Detected;
        public event ObjectDetectionHandler DetectionReleased;

        private readonly List<Detector> _detectors = new();

        private void OnDestroy()
        {
            NotifyDetectors();
        }

        public void Detect(GameObject detectionSource)
        {
            Detected?.Invoke(detectionSource, gameObject);
            _detectors.Add(detectionSource.GetComponent<Detector>());
        }

        public void ReleaseDetection(GameObject detectionSource)
        {
            DetectionReleased?.Invoke(detectionSource, gameObject);
            _detectors.Remove(detectionSource.GetComponent<Detector>());
        }

        private void NotifyDetectors()
        {
            foreach (var detector in _detectors) 
                detector.ReleaseDetection(gameObject);
        }
    }
}