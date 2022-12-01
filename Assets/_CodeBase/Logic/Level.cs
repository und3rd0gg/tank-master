using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform TransitionConnectionPoint { get; private set; }
    }
}