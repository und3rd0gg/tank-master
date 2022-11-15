using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class Enemy : MonoBehaviour, IActor
    {
        [field: SerializeField] public Health Health { get; protected set; }
    }
}