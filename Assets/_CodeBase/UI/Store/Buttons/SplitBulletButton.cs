using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public class SplitBulletButton : UpgradeButton
    {
        protected override void OnUpgrade()
        {
            var bulletShooter = AllServices.Container.Single<IGameFactory>().PlayerGameObject.GetComponent<Player>()
                .Shooter;
            bulletShooter.UpgradeBulletCount(UpgradeInfo[BoughtUpgradeLevel].Value);
        }
    }
}