using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    public class LoadLevelTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Transform _levelConnectionPoint;

        private IGameFactory _gameFactory;

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
            AllServices.Container.Single<IGameFactory>().CreateLevel(_levelConnectionPoint.position);
            enabled = false;
        }
    }
}