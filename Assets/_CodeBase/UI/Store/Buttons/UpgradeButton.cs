using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    public abstract class UpgradeButton : StoreItemButton
    {
        [SerializeField] private TMP_Text _pricePresenter;
        [SerializeField] private Image[] _stars;
        [SerializeField] private GameObject _glow;

        private Color _disabledStarColor = new(0.51f, 0.51f, 0.51f);
        private Color _enabledStarColor = Color.white;

        protected Dictionary<uint, uint> PriceMap = new()
        {
            [1] = 100,
            [2] = 200,
            [3] = 500,
        };

        public uint CurrentPrice { get; protected set; } = 100;
        public uint UpgradeLevel { get; protected set; } = 0;
        public int MaxLevel => PriceMap.Count;

        public override void OnPointerClick(PointerEventData eventData)
        {
            IncreasePrice();
            UpdatePresenter(CurrentPrice);
            UpdateStars();
            CheckMaxLevelReach();
            base.OnPointerClick(eventData);
        }

        private void CheckMaxLevelReach()
        {
            if (UpgradeLevel == MaxLevel)
                _glow.SetActive(true);
        }

        private void UpdateStars()
        {
            var starsCountToEnable = UpgradeLevel;

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

        private void IncreasePrice()
        {
            if (UpgradeLevel + 1 <= MaxLevel)
                UpgradeLevel++;
            else
            {
                return;
            }

            CurrentPrice = PriceMap[UpgradeLevel];
        }

        private void UpdatePresenter(uint value)
        {
            _pricePresenter.text = value.ToString();
        }
    }
}