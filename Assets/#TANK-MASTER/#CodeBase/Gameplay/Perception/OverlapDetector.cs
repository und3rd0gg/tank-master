using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TankMaster.Gameplay.Perception
{
    [Serializable]
    public class OverlapDetector
    {
        [SerializeField] private float _radius;
        [SerializeField] private Transform _overlapPoint;
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private int _bufferSize;

        private Collider[] _buffer;
        private List<Collider> _collidersInside;

        public event Action<Collider> OnDetected = delegate { };
        public event Action<Collider> OnDetectionExit = delegate { };

        public void Init() {
            _buffer = new Collider[_bufferSize];
            _collidersInside = new List<Collider>(_bufferSize);
        }

        public void Detect() {
            Physics.OverlapSphereNonAlloc(_overlapPoint.position, _radius, _buffer, _enemyMask);

            for (var i = 0; i < _buffer.Length; i++) {
                Collider col = _buffer[i];

                if (!_collidersInside.Contains(col)) {
                    _collidersInside.Add(col);
                    OnDetectionRadiusEnter(col);
                }
            }

            for (int i = _collidersInside.Count - 1; i >= 0; i--) {
                if (!_buffer.Contains(_collidersInside[i])) {
                    OnDetectionRadiusExit(_collidersInside[i]);
                    _collidersInside.RemoveAt(i);
                }
            }
        }

        private void OnDetectionRadiusExit(Collider col) {
            OnDetected(col);
        }

        private void OnDetectionRadiusEnter(Collider col) {
            OnDetectionExit(col);
        }

#if UNITY_EDITOR
        
        public void TryDrawGizmos() {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(_overlapPoint.position, _radius);
        }

#endif
    }
}