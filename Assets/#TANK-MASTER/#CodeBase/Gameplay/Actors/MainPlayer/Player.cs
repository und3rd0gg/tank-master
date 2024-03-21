using TankMaster.Data;
using TankMaster.Gameplay.Perception;
using TankMaster.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class Player : ActorBase, IProgressSaver
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        [field: SerializeField] public OverlapDetector OuterRadiusDetector { get; private set; }
        [field: SerializeField] public BulletShooter BulletShooter { get; private set; }
        [field: SerializeField] public BulletShooter MissileShooter { get; private set; }
        
        [SerializeField] private Money _money;
        
        public Money Money => _money;

        private void Awake() {
            OuterRadiusDetector.Init();
        }

        private void Update() {
            OuterRadiusDetector.Detect();
        }

        private void OnDrawGizmos() {
            OuterRadiusDetector.TryDrawGizmos();
        }

        public void LoadProgress(PlayerProgress playerProgress) => 
            _money.LoadProgress(playerProgress);

        public void UpdateProgress(PlayerProgress playerProgress) => 
            _money.UpdateProgress(playerProgress);
    }
}