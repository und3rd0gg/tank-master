using System;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.UI;
using TankMaster._CodeBase.UI.Panels;
using TankMaster._CodeBase.UI.Store;
using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    public class StoreOpener : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private Panel _store;

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
            //_store ??= AllServices.Container.Single<IGameFactory>().Interface.GetComponentInChildren<Store>();
            AllServices.Container.Single<IGameFactory>().Interface.GetComponent<Interface>().Store.Enable();
        }
    }
}