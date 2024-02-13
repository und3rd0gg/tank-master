using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.UI;
using TankMaster.UI.Panels;
using UnityEngine;
using VContainer;

namespace TankMaster.Logic
{
    public class StoreOpener : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private Panel _store;
        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += OpenShopWindow;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OpenShopWindow;
        }

        private void OpenShopWindow(Collider obj)
        {
            enabled = false;
            _store ??= _gameFactory.Interface.GetComponent<Interface>().Store;
            _store.Enable();
        }
    }
}