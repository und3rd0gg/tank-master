using System;
using TankMaster.Gameplay.Actors;
using UnityEngine;

namespace TankMaster.Gameplay
{
    [Serializable]
    public class Health : IActorAttribute<int>
    {
        [field: SerializeField] public int MaxValue { get; private set; } = 100;
        [field: SerializeField] public int Value { get; private set; } = 100;

        public event Action<int, int> ValueChanged;
        public event Action<Health> Died;

        private void OnValidate()
        {
            if (Value > MaxValue)
                Value = MaxValue;
        }

        public virtual void ApplyDamage(int damage)
        {
            var newValue = Value - damage;

            if (newValue <= 0)
            {
                Died?.Invoke(this);
                newValue = 0;
            }

            Value = newValue;
            ValueChanged?.Invoke(Value, MaxValue);
        }

        public void UpgradeMaxValue(int newValue)
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