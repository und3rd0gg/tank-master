using System;

namespace TankMaster._CodeBase.Gameplay
{
    public interface IСharacterСharacteristic
    {
        public uint MaxValue { get; }
        public uint Value { get; }

        public event Action<uint, uint> ValueChanged;
    }
}