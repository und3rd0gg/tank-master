using System;
using UnityEngine;

namespace TankMaster.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [SerializeField] public StoreData StoreData;
        [SerializeField] public uint MoneyBalance;
    }

    [Serializable]
    public class StoreData
    {
        
    }
}