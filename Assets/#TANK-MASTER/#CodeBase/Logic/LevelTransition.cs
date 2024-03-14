using TankMaster.Gameplay.Barriers;
using UnityEngine;

namespace TankMaster.Logic
{
    public sealed class LevelTransition : LevelBase
    {
        [field: SerializeField] public EnterBarrier EnterBarrier { get; private set; }
    }
}