using TankMaster._CodeBase.Gameplay.Barriers;
using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    public class LevelTransition : MonoBehaviour
    {
        [field: SerializeField] public EnterBarrier EnterBarrier { get; private set; }
    }
}