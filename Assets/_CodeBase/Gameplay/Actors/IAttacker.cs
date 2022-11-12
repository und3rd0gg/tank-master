using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors
{
    public interface IAttacker
    {
        public bool enabled { get; set; }
        
        public void Attack(Transform target);
        public void SetTarget(Transform target);
        public bool IsInEffectiveDistance();
    }
}