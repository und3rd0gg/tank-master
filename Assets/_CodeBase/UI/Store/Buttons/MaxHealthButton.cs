using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public class MaxHealthButton : UpgradeButton
    {
        protected override void OnUpgrade()
        {
            var playerHealth = AllServices.Container.Single<IGameFactory>().PlayerGameObject
                .GetComponentInChildren<Player>().Health;
            playerHealth.UpgradeMaxValue((uint) UpgradeInfo[BoughtUpgradeLevel].Value);
            playerHealth.RestoreHealth();
        }
    }
}