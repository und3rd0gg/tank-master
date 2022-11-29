using Agava.YandexGames;
using UnityEngine;

namespace TankMaster
{
    public class RandomUpgradeButton : StoreItemButton
    {
        [SerializeField] private StoreItemButton[] _buttons;

        public override void OnClick()
        {
            var button = GetRandomButton();
            VideoAd.Show(onCloseCallback: button.OnClick);
        }

        private StoreItemButton GetRandomButton() => 
            _buttons[Random.Range(0, _buttons.Length)];
    }
}