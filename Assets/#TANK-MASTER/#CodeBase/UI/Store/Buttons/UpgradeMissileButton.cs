using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public class UpgradeMissileButton : UpgradeButton
    {
        protected override void OnUpgrade()
        {
            var missileShooter = AllServices.Container.Single<IGameFactory>().PlayerGameObject.GetComponent<Player>()
                .MissileShooter;

            switch (BoughtUpgradeLevel)
            {
                case 1:
                    missileShooter.ShouldShoot = true;
                    break;
                case 2:
                    missileShooter.UpgradeShootDelay(1.2f);
                    break;
                case 3:
                    missileShooter.UpgradeShootDelay(0.85f);
                    break;
            }
        }
    }
}