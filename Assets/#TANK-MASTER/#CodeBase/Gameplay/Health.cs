using System;
using UnityEngine;

namespace TankMaster.Gameplay
{
    [Serializable]
    public class Health : IСharacterСharacteristic
    {
        [field: SerializeField] public uint MaxValue { get; private set; } = 100;
        [field: SerializeField] public uint Value { get; private set; } = 100;

        public event Action<uint, uint> ValueChanged;
        public event Action<Health> Died;

        private void OnValidate()
        {
            if (Value > MaxValue)
                Value = MaxValue;
        }

        public virtual void ApplyDamage(uint damage)
        {
            var newValue = (int) (Value - damage);

            if (newValue <= 0)
            {
                Died?.Invoke(this);
                newValue = 0;
            }

            Value = (uint) newValue;
            ValueChanged?.Invoke(Value, MaxValue);
        }

        public void UpgradeMaxValue(uint newValue)
        {
            MaxValue = newValue;
            ValueChanged?.Invoke(Value, MaxValue);
        }

        public void RestoreHealth()
        {
            Value = MaxValue;
            ValueChanged?.Invoke(Value, MaxValue);
        }
    }
}