using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure
{
    [RequireComponent(typeof(BoxCollider))]
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField][Attach] private BoxCollider _collider;
    }
}