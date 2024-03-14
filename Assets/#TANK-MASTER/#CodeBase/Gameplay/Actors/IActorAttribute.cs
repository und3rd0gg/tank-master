using System;

namespace TankMaster.Gameplay.Actors
{
    public interface IActorAttribute<TAttribute>
    {
        public TAttribute MaxValue { get; }
        public TAttribute Value { get; }

        public event Action<TAttribute, TAttribute> ValueChanged;
    }
}