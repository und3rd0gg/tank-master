using UnityEngine;
using UnityEngine.UI;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public abstract class UpgradeButton : StoreItemButton
    {
        [SerializeField] private Image[] _stars;
        [SerializeField] private GameObject _glow;

        private readonly Color _disabledStarColor = new(0.51f, 0.51f, 0.51f);
        private readonly Color _enabledStarColor = Color.white;

        public uint CurrentPrice => UpgradeInfo[BoughtUpgradeLevel + 1].Price;
        public int MaxLevel => UpgradeInfo.Count;

        protected override bool BuyCondition => 
            !MaxLevelReached && PlayerMoney.HasEnough(UpgradeInfo[NextUpgradeLevel].Price);

        private bool MaxLevelReached =>
            BoughtUpgradeLevel == MaxLevel;

        public override void OnClick()
        {
            if (!BuyCondition)
            {
                PlayErrorSound();
                return;
            }
                
            SpendMoney();
            IncreaseUpgradeLevel();
            OnUpgrade();
            PlayBuySound();
            UpdateStars();
            EnableGlowIfAppropriate();
            UpdatePresenter();
        }

        protected abstract void OnUpgrade();

        private void UpdatePresenter()
        {
            if (MaxLevelReached)
            {
                UpdatePresenter(null);
            }
            else
            {
                UpdatePresenter(CurrentPrice);
            }
        }

        private void EnableGlowIfAppropriate()
        {
            if (MaxLevelReached)
                _glow.SetActive(true);
        }

        private void UpdateStars()
        {
            var starsCountToEnable = BoughtUpgradeLevel;

            for (var i = 0; i < _stars.Length; i++)
            {
                if (starsCountToEnable > 0)
                {
                    _stars[i].color = _enabledStarColor;
                    starsCountToEnable--;
                }
                else
                {
                    _stars[i].color = _disabledStarColor;
                }
            }
        }

        private void IncreaseUpgradeLevel()
        {
            if (!MaxLevelReached)
                BoughtUpgradeLevel = NextUpgradeLevel;
        }
    }
}