using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using VContainer;

namespace TankMaster.UI.Store.Buttons
{
    public class SplitBulletButton : UpgradeButton
    {
        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }
        
        protected override void OnUpgrade()
        {
            var bulletShooter = _gameFactory.PlayerGameObject.GetComponent<Player>()
                .BulletShooter;
            bulletShooter.UpgradeBulletCount(UpgradeInfo[BoughtUpgradeLevel].Value);
        }
    }
}