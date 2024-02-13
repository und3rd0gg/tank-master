namespace TankMaster.Gameplay
{
    public interface IDeactivateable
    {
        public bool enabled { get; set; }

        private void Deactivate()
        {
            enabled = false;
        }
    }
}