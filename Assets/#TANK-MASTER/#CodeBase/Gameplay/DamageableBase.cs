using UnityEngine;

namespace TankMaster.Gameplay
{
    public abstract class DamageableBase : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public Health Health { get; protected set; }
    }

    public abstract class ActorBase : DamageableBase, IActor
    {
        [field: SerializeField] public Transform Head { get; protected set; }
        [field: SerializeField] public Transform Chest { get; protected set; }
        [field: SerializeField] public Transform Pivot { get; protected set; }
    }
}