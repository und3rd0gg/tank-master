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
            var carHealth = Player.Health;
            carHealth.RestoreHealth();
        }
    }
}