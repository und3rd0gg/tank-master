using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace TankMaster.Gameplay.Tutorial
{
    [Serializable]
    public class DialogNode
    {
        [field: SerializeField] public LocalizedString Text { get; private set; }
        [field: SerializeField] public int NextNodeIndex { get; private set; }
        [field: SerializeField] public bool LastNode { get; private set; }
        
        public UnityEvent OnClose;
    }
}