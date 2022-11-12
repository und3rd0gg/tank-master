using Dythervin.AutoAttach;
using TankMaster._CodeBase.StaticData;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class Enemy : MonoBehaviour, IActor
    {
        [field: SerializeField] public EnemyProfile EnemyProfile;
        [field: SerializeField][field: Attach] public Health Health { get; protected set; }
    }
}