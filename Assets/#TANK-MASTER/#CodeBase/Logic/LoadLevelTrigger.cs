using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.Logic
{
    public class LoadLevelTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Transform _levelConnectionPoint;

        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += TriggerObserverOnTriggerEnter;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerObserverOnTriggerEnter;
        }

        private void TriggerObserverOnTriggerEnter(Collider obj)
        {
            _gameFactory.CreateLevel(_levelConnectionPoint.position);
            enabled = false;
        }
    }
}