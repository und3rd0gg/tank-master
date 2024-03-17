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
        private IEnvFactory _envFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory, IEnvFactory envFactory) {
            _envFactory = envFactory;
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
            _envFactory.CreateLevel(_levelConnectionPoint.position);
            enabled = false;
        }
    }
}