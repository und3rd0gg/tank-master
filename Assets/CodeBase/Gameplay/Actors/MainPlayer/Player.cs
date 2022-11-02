using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class Player : MonoBehaviour, IActor, IDamageable
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        [SerializeField] private Health _health;
        [SerializeField] private ShootProfile _shootProfile;
        [SerializeField] private Shooter _shooter;

        public uint Health => _health.Value;

        public void ApplyDamage(uint damage) => 
            _health.Value -= damage;

        private void Awake()
        {
            InitializeShooter();
        }

        private void InitializeShooter() => 
            _shooter.SetShootProfile(_shootProfile);
    }
}
