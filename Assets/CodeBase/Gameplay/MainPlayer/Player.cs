using UnityEngine;

namespace TankMaster.Gameplay.MainPlayer
{
    public class Player : MonoBehaviour, IActor, IDamageable
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        [SerializeField] private Health _health;

        public uint Health => _health.Value;

        public void ApplyDamage(uint damage)
        {
            _health.Value -= damage;
        }
    }
}
