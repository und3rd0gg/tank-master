using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public class RepairButton : StoreItemButton
    {
        public override void OnClick()
        {
            throw new System.NotImplementedException();
        }

        private void RepairCar()
        {
            var carHealth = AllServices.Container.Single<IGameFactory>().PlayerGameObject
                .GetComponentInChildren<Player>().Health;
            carHealth.RestoreHealth();
        }
    }
}