using TankMaster.Gameplay.Barriers;
using UnityEngine;

namespace TankMaster.Logic
{
    public class LevelTransition : MonoBehaviour
    {
        [field: SerializeField] public EnterBarrier EnterBarrier { get; private set; }
    }
}