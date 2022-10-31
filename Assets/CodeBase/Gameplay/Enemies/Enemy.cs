using System;
using UnityEngine;

namespace TankMaster.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour, IActor, IDamageable
    {
        [field: SerializeField] public EnemyProfile EnemyProfile;
        
        public uint Health { get; }
        
        public void ApplyDamage(uint damage)
        {
            throw new System.NotImplementedException();
        }
    }
}