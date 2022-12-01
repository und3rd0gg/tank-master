using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public abstract class StoreItemButton : MonoBehaviour, IPointerClickHandler
    {
        private Dictionary<uint, uint> _priceMap = new()
        {
            [1] = 100,
            [2] = 200,
            [3] = 500,
        };
        private uint _currentUpgradeLevel;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }

        public abstract void OnClick();
    }
}
