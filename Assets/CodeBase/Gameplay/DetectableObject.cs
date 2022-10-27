using TankMaster.AIPerception;
using UnityEngine;

namespace TankMaster.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class DetectableObject : MonoBehaviour, IDetectableObject
    {
        public event ObjectDetectionHandler Detected;
        public event ObjectDetectionHandler DetectionReleased;
        
        public void Detect(GameObject detectionSource)
        {
            Detected?.Invoke(detectionSource, gameObject);
        }

        public void ReleaseDetection(GameObject detectionSource)
        {
            DetectionReleased?.Invoke(detectionSource, gameObject);
        }
    }
}