
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure
{
    [RequireComponent(typeof(BoxCollider))]
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField]private BoxCollider _collider;
    }
}