using BuildingBlocks.DataTypes;
using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster
{
    public class MaxHealthButton : UpgradeButton
    {
        [SerializeField] private InspectableDictionary<uint, uint> UpgradeMap;

        public override void OnClick()
        {
            UpgradePlayerHealth();
        }

        private void UpgradePlayerHealth()
        {
            var playerHealth = AllServices.Container.Single<IGameFactory>().PlayerGameObject
                .GetComponentInChildren<Player>().Health;
            playerHealth.UpgradeMaxValue(UpgradeMap[UpgradeLevel]);
        }
    }
}