using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using VContainer;

namespace TankMaster.UI.Store.Buttons
{
    public class IncreaseIncomeButton : UpgradeButton
    {
        
        protected override void OnUpgrade()
        {
            var money = GameFactory.PlayerGameObject.GetComponentInChildren<Player>()
                .Money;
            money.UpgradeMultiplier(UpgradeInfo[BoughtUpgradeLevel].Value);
        }
    }
}