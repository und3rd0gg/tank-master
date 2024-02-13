using TankMaster.Gameplay.Actors.MainPlayer;

namespace TankMaster.UI.Store.Buttons
{
    public class RepairButton : OneTimeButton
    {
        protected override void OnUpgrade()
        {
            RepairCar();
        }

        private void RepairCar()
        {
            var carHealth = GameFactory.PlayerGameObject
                .GetComponentInChildren<Player>().Health;
            carHealth.RestoreHealth();
        }
    }
}