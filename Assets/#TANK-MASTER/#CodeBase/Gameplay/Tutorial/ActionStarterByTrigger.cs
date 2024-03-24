using TankMaster.Common;
using TankMaster.Common.Physics;
using UnityEngine;
using UnityEngine.Events;

namespace TankMaster.Gameplay.Tutorial
{
    public class ActionStarterByTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private UnityEvent _action;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider obj)
        {
            _action?.Invoke();
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnTriggerEnter;
        }
    }
}