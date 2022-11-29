using System.Collections.Generic;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors
{
    public class PhysicsDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _detectionMask;
        [SerializeField] private float _detectionRadius;
        [SerializeField] [Min(1)] private int _bufferSize;

        private Collider[] _buffer;

        public IReadOnlyCollection<Collider> DetectedObjects => _buffer;

        private void Start()
        {
            _buffer = new Collider[_bufferSize];
        }

        private void FixedUpdate()
        {
            Detect();
        }

        public List<Collider> GetDetectedObjects()
        {
            List<Collider> detectedObjects = new List<Collider>();

            foreach (var col in _buffer)
            {
                if (col != null)
                    detectedObjects.Add(col);
            }

            return detectedObjects;
        }

        public Transform GetClosestObject()
        {
            Transform _closestObject;

            if (_buffer[0] == null)
                return null;
            
            _closestObject = _buffer[0].transform;
        
            for (int i = 1; i < _buffer.Length; i++)
            {
                if(_buffer[i] == null)
                    continue;
                
                var distanceToClosestObject = Vector3.Distance(transform.position, _closestObject.position);
                var distanceToCurrentObject =
                    Vector3.Distance(transform.position, _buffer[i].transform.position);
        
                if (distanceToCurrentObject < distanceToClosestObject)
                    _closestObject = _buffer[i].transform;
            }
        
            return _closestObject;
        }

        private void Detect()
        {
            ClearBuffer();
            Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, _buffer, _detectionMask);
        }

        private void ClearBuffer()
        {
            for (var i = 0; i < _buffer.Length; i++)
                _buffer[i] = null;
        }

        private void OnDrawGizmos()
        {
            DrawWireSphere(transform.position, _detectionRadius, Color.yellow);
            
            void DrawWireSphere(Vector3 center, float radius, Color color)
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(center, radius);
            }
        }
    }
}