using AYellowpaper;
using TankMaster._CodeBase.Data;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TankMaster._CodeBase.UI.Panels;
using UnityEngine;

namespace TankMaster._CodeBase.UI.Store
{
    public class Store : Panel, IProgressSaver
    {
        [SerializeField] private InterfaceReference<IProgressSaver>[] StoreItems;

        private Interface _interface;
        
        public override void Enable()
        {
            base.Enable();
            AllServices.Container.Single<IInputService>().HideVisuals();
            _interface ??= AllServices.Container.Single<IGameFactory>().Interface.GetComponent<Interface>();
            _interface.BalancePresenter.Open();
        }

        public override void Disable()
        {
            base.Disable();
            AllServices.Container.Single<IInputService>().ShowVisuals();
            _interface.BalancePresenter.Close();
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            throw new System.NotImplementedException();
        }
    }
}