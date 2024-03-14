using UnityEngine;

namespace TankMaster.Logic
{
    public class Level : LevelBase
    {
        [field: SerializeField] public Transform TransitionConnectionPoint { get; private set; }
    }
}