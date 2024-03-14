using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using VContainer;

namespace TankMaster.UI.Store.Buttons
{
    public class MaxHealthButton : UpgradeButton
    {
        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }
        
        protected override void OnUpgrade()
        {
            var playerHealth = _gameFactory.PlayerGameObject
                .GetComponentInChildren<Player>().Health;
            playerHealth.UpgradeMaxValue(UpgradeInfo[BoughtUpgradeLevel].Value);
            playerHealth.RestoreHealth();
        }
    }
}