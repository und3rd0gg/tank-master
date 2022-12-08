using System;

namespace TankMaster._CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public string LastLevel { get; set; }
        public uint MoneyBalance { get; set; }

        public PlayerProgress(string initialLevel)
        {
            LastLevel = initialLevel;
        }
    }
}