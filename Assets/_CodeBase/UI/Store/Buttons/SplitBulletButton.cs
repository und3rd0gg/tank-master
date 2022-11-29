using BuildingBlocks.DataTypes;
using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster
{
    public class SplitBulletButton : UpgradeButton
    {
        [SerializeField] private InspectableDictionary<uint, int> UpgradeMap;
        
        private BulletShooter _bulletShooter;

        private void Awake()
        {
            AllServices.Container.Single<IGameFactory>().PlayerCreated += OnPlayerCreated;
        }

        private void OnPlayerCreated()
        {
            var gameFactory = AllServices.Container.Single<IGameFactory>();
            _bulletShooter = gameFactory.PlayerGameObject
                .GetComponentInChildren<BulletShooter>();
            gameFactory.PlayerCreated -= OnPlayerCreated;
        }

        public override void OnClick()
        {
            _bulletShooter.UpgradeBulletCount(UpgradeMap[UpgradeLevel]);
        }
    }
}