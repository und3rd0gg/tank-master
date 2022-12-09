using TankMaster._CodeBase.Data;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    public class Player : MonoBehaviour, IActor, IProgressSaver
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        public Health Health => _playerHealth;
        public Money Money => _money;
        
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Money _money;

        public void LoadProgress(PlayerProgress playerProgress) => 
            _money.LoadProgress(playerProgress);

        public void UpdateProgress(PlayerProgress playerProgress) => 
            _money.UpdateProgress(playerProgress);
    }
}