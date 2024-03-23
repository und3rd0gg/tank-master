using TankMaster.Data;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.UI.Panels;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.Store
{
    public class Store : Panel, IProgressSaver
    {
        //[SerializeField] private InterfaceReference<IProgressSaver>[] StoreItems;

        private Interface _interface;
        private IInputService _inputService;
        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IInputService inputService, IGameFactory gameFactory) {
            _gameFactory = gameFactory;
            _inputService = inputService;
        }
        
        public override void Enable()
        {
            base.Enable();
            _inputService.HideVisuals();
            _interface ??= _gameFactory.Interface.GetComponent<Interface>();
            _interface.BalancePresenter.Open();
        }

        public override void Disable()
        {
            base.Disable();
            _inputService.ShowVisuals();
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