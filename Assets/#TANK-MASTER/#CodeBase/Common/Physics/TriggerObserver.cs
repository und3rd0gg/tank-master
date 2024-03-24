using System;
using UnityEngine;

namespace TankMaster.Common.Physics
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnEnable() =>
            _collider.enabled = true;

        public void Disable() =>
            _collider.enabled = false;

        private void OnTriggerEnter(Collider other) =>
            TriggerEnter?.Invoke(other);

        private void OnTriggerExit(Collider other) =>
            TriggerExit?.Invoke(other);
    }
}