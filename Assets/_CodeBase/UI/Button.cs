using UnityEngine;
using UnityEngine.EventSystems;

namespace TankMaster._CodeBase.UI
{
    public abstract class Button : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }

        protected abstract void OnClick();
    }
}