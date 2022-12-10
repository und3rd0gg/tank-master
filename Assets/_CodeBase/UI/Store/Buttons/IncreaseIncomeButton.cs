using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public class IncreaseIncomeButton : UpgradeButton
    {
        protected override void OnUpgrade()
        {
            var money = AllServices.Container.Single<IGameFactory>().PlayerGameObject.GetComponentInChildren<Player>()
                .Money;
            money.UpgradeMultiplier(UpgradeInfo[BoughtUpgradeLevel].Value);
        }
    }
}