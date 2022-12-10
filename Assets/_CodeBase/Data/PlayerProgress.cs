using System;
using UnityEngine;

namespace TankMaster._CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [SerializeField] public StoreData StoreData;
        
        [field: SerializeField] public uint MoneyBalance { get; set; }
    }

    [Serializable]
    public class StoreData
    {
        
    }
}