using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    public class Player : MonoBehaviour, IActor
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        public Health Health => _playerHealth;
        public Money Money => _money;
        
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Money _money;
        
    }
}