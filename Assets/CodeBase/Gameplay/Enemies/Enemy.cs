using UnityEngine;

namespace TankMaster.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour, IActor, IDamageable
    {
        public uint Health { get; }
        public void ApplyDamage(uint damage)
        {
            throw new System.NotImplementedException();
        }
    }
}