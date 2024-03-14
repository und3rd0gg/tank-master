using UnityEngine;

namespace TankMaster.Gameplay
{
    public interface IActor : IDamageable
    {
        public Transform Head { get; }
        public Transform Chest { get; }
    }
}