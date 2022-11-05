using Dythervin.AutoAttach;
using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class Player : MonoBehaviour, IActor
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        [field: SerializeField][field: Attach] public Health Health { get; private set; }
        [SerializeField] private ShootProfile _shootProfile;
        [SerializeField] private Shooter _shooter;

        private void Awake()
        {
            InitializeShooter();
        }

        private void InitializeShooter() => 
            _shooter.SetShootProfile(_shootProfile);
    }
}
