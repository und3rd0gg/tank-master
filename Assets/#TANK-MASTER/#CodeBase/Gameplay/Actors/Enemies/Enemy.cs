using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public class Enemy : MonoBehaviour, IActor
    {
        [field: SerializeField] public Health Health { get; protected set; }
    }
}