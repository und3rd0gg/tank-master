using System;
using System.Collections.Generic;
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
            int numColliders = Physics.OverlapSphereNonAlloc(_overlapPoint.position, _radius, _buffer, _enemyMask);

            for (int i = _collidersInside.Count - 1; i >= 0; i--) {
                Collider col = _collidersInside[i];
                var stillInside = false;

                for (int j = 0; j < numColliders; j++) {
                    if (_buffer[j] == col) {
                        stillInside = true;
                        break;
                    }
                }

                if (!stillInside) {
                    _collidersInside.RemoveAt(i);
                    OnDetectionRadiusExit(col);
                }
            }

            for (var i = 0; i < numColliders; i++) {
                Collider col = _buffer[i];

                if (!_collidersInside.Contains(col)) {
                    _collidersInside.Add(col);
                    OnDetectionRadiusEnter(col);
                }
            }
        }

        private void OnDetectionRadiusExit(Collider col) {
            OnDetectionExit(col);
        }

        private void OnDetectionRadiusEnter(Collider col) {
            OnDetected(col);
        }

#if UNITY_EDITOR

        public void TryDrawGizmos() {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(_overlapPoint.position, _radius);
        }

#endif
    }
}