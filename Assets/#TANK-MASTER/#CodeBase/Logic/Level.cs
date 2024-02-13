using UnityEngine;

namespace TankMaster.Logic
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform TransitionConnectionPoint { get; private set; }
    }
}