using System;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.UI.Panels;

namespace TankMaster._CodeBase.UI.Store
{
    public class Store : Panel
    {
        private Interface _interface;
        
        public override void Enable()
        {
            base.Enable();
            _interface ??= AllServices.Container.Single<IGameFactory>().Interface.GetComponent<Interface>();
            _interface.BalancePresenter.Open();
        }

        public override void Disable()
        {
            base.Disable();
            _interface.BalancePresenter.Close();
        }
    }
}