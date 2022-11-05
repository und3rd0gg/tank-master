using System;

namespace TankMaster.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public string LastLevel { get; set; }

        public PlayerProgress(string initialLevel)
        {
            LastLevel = initialLevel;
        }
    }
}