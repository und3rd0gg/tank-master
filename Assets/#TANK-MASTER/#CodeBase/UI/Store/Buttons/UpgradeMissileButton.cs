using TankMaster.Gameplay.Actors.MainPlayer;

namespace TankMaster.UI.Store.Buttons
{
    public class UpgradeMissileButton : UpgradeButton
    {
        protected override void OnUpgrade()
        {
            var missileShooter = Player.MissileShooter;

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