using System;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies.Settings
{
    [Serializable]
    public class PatrolSettings
    {
        [field: SerializeField] public float PatrolRadius { get; private set; }
        [field: SerializeField] public float WaitTime { get; private set; }
    }
}