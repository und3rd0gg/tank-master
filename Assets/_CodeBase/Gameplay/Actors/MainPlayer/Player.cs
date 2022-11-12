using Dythervin.AutoAttach;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    public class Player : MonoBehaviour, IActor
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }

        [field: SerializeField]
        [field: Attach]
        public Health Health { get; private set; }

        [FormerlySerializedAs("_shootProfile")] [SerializeField] private AttackProfile _attackProfile;
    }
}