using System.Collections.Generic;
using UnityEngine;

namespace TankMaster.AIPerception
{
    public delegate void ObjectDetectionHandler(GameObject source, GameObject detectedObject);
    
    public interface IDetector
    {
        abstract LayerMask _detectables { get; }

        public event ObjectDetectionHandler ObjectDetected;
        public event ObjectDetectionHandler DetectionReleased;

        public void Detect(IDetectableObject detectedObject);
        public void Detect(GameObject detectedObject);

        public void ReleaseDetection(IDetectableObject detectedObject);
        public void ReleaseDetection(GameObject detectedObject);
    }
}