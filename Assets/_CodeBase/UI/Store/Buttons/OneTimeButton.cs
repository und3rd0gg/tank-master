using UnityEngine;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public abstract class OneTimeButton : StoreItemButton
    {
        [SerializeField] private uint _price;

        protected override bool BuyCondition =>
            PlayerMoney.HasEnough(_price);

        private void OnEnable()
        {
            Button.interactable = true;
        }

        public override void OnClick()
        {
            if (BuyCondition)
            {
                PlayBuySound();
                OnUpgrade();
                Button.interactable = false;
            }
            else
            {
                PlayErrorSound();
            }
        }

        protected abstract void OnUpgrade();
    }
}