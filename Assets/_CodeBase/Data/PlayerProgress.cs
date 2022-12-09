using System;
using UnityEngine;

namespace TankMaster._CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [field: SerializeField] public uint MoneyBalance { get; set; }
    }
}