using BuildingBlocks.DataTypes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public abstract class StoreItemButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected InspectableDictionary<int, int> PriceMap;

        private uint _currentUpgradeLevel;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }

        public abstract void OnClick();
    }
}
