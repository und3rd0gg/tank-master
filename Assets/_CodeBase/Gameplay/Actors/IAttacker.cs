using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors
{
    public interface IAttacker : IDeactivateable
    {
        public void SetTarget(Transform target);
    }
}