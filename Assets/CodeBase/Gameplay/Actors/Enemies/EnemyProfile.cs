using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    [CreateAssetMenu(fileName = "New Enemy Profile", menuName = "Gameplay/Enemy Profile", order = 0)]
    public class EnemyProfile : ScriptableObject
    {
        [field: SerializeField] public float ChaseCooldown { get; private set; } = 3f;
        [field: SerializeField] public ShootProfile ShootProfile { get; private set; }
    }
}