using UnityEngine;

namespace TankMaster._CodeBase.AIPerception
{
    public interface IDetectableObject
    {
        public GameObject gameObject { get; }
        
        public event ObjectDetectionHandler Detected;
        public event ObjectDetectionHandler DetectionReleased;

        public void Detect(GameObject detectionSource);
        public void ReleaseDetection(GameObject detectionSource);
    }
}