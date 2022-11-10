using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster.Infrastructure
{
    [RequireComponent(typeof(BoxCollider))]
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField][Attach] private BoxCollider _collider;
    }
}