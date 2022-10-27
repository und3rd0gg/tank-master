using UnityEngine;

namespace TankMaster.Gameplay.MainPlayer
{
    public class Player : MonoBehaviour, IActor
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
    }
}
