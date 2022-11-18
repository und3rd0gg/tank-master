using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    public class Player : MonoBehaviour, IActor
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        public Health Health => _playerHealth;

        [SerializeField] private PlayerHealth _playerHealth;
    }
}