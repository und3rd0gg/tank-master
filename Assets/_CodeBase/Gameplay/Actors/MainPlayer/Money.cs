using System;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    [Serializable]
    public class Money : IСharacterСharacteristic
    {
        [field: SerializeField] public uint Value { get; private set; }

        public uint Multiplier { get; private set; } = 1;
        public uint MaxValue => uint.MaxValue;

        public event Action<uint, uint> ValueChanged;

        public void Add(uint amount) =>
            Value += amount * Multiplier;

        public void UpgradeMultiplier(int newMultiplier)
        {
            if (newMultiplier < Multiplier)
                throw new ArgumentException("Wrong Value Provided");

            Multiplier = (uint) newMultiplier;
        }

        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Invalid amount");

            Add((uint) amount);
            ValueChanged?.Invoke(Value, MaxValue);
        }
    }
}