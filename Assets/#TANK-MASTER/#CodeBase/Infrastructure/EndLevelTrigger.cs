using UnityEngine;

namespace TankMaster.Infrastructure
{
    [RequireComponent(typeof(BoxCollider))]
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
    }
}