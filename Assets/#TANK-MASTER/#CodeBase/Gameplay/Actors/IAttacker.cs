using UnityEngine;

namespace TankMaster.Gameplay.Actors
{
    public interface IAttacker : IDeactivateable
    {
        public void SetTarget(Transform target);
    }
}