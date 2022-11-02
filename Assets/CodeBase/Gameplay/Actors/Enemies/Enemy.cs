using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public class Enemy : MonoBehaviour, IActor, IDamageable
    {
        [field: SerializeField] public EnemyProfile EnemyProfile;
        [SerializeField][Attach] private Shooter _shooter;

        public uint Health { get; }

        private void Awake()
        {
            InitializeShooterComponent();
        }

        private void InitializeShooterComponent() => 
            _shooter.SetShootProfile(EnemyProfile.ShootProfile);

        public void ApplyDamage(uint damage)
        {
            throw new System.NotImplementedException();
        }
    }
}