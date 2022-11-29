using BuildingBlocks.DataTypes;
using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster
{
    public class IncreaseIncomeButton : UpgradeButton
    {
        [SerializeField] private InspectableDictionary<uint, int> UpgradeMap;
        
        private Money _money;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.PlayerGameObject != null)
                InitializeMoney();
            else
                _gameFactory.PlayerCreated += InitializeMoney;
        }

        public override void OnClick()
        {
            _money.UpgradeMultiplier(UpgradeMap[UpgradeLevel]);
            Debug.Log($"money multiplier {_money.Multiplier}");
        }

        private void InitializeMoney()
        {
            _money = _gameFactory.PlayerGameObject.GetComponentInChildren<Player>().Money;
        }
    }
}