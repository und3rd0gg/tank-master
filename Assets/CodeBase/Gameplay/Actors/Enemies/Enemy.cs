using Dythervin.AutoAttach;
using TankMaster.StaticData;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public class Enemy : MonoBehaviour, IActor
    {
        [field: SerializeField] public EnemyProfile EnemyProfile;
        [field: SerializeField][field: Attach]public Health Health { get; private set; }
        [SerializeField][Attach] private EnemyShooter _shooter;

        private void Awake()
        {
            InitializeShooterComponent();
        }

        private void InitializeShooterComponent() => 
            _shooter.SetShootProfile(EnemyProfile.ShootProfile);
    }
}