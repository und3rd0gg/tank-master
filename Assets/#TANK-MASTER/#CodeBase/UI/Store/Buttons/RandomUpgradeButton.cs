using UnityEngine;

namespace TankMaster.UI.Store.Buttons
{
    public class RandomUpgradeButton : StoreItemButton
    {
        private IUpgradeButton[] _buttons;

        protected override void Awake()
        {
            base.Awake();
            _buttons = transform.parent.GetComponentsInChildren<IUpgradeButton>();
        }

        public override void OnClick()
        {
            var button = GetRandomButton();
            //VideoAd.Show(onCloseCallback: button.Upgrade);
        }
    
        private IUpgradeButton GetRandomButton() => 
            _buttons[Random.Range(0, _buttons.Length)];
    }
}