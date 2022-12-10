using System;
using BuildingBlocks.DataTypes;
using Dythervin.AutoAttach;
using TankMaster._CodeBase.Data;
using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class StoreItemButton : MonoBehaviour, IPointerClickHandler, IProgressSaver
    {
        [SerializeField] [Attach] private AudioSource _audioSource;
        
        [SerializeField] protected InspectableDictionary<uint, UpgradeInfo> UpgradeInfo;
        [SerializeField] protected TMP_Text PricePresenter;
        [SerializeField] private AudioClip BuySound;
        [SerializeField] private AudioClip ErrorSound;

        private Money _playerMoney;

        public uint BoughtUpgradeLevel = 0;

        protected Money PlayerMoney => _playerMoney ??= AllServices.Container.Single<IGameFactory>().PlayerGameObject
            .GetComponent<Player>()
            .Money;
        protected uint NextUpgradeLevel =>
            BoughtUpgradeLevel + 1;

        protected virtual bool BuyCondition { get; } 

        protected virtual void Awake()
        {
            UpdatePresenter(UpgradeInfo[NextUpgradeLevel].Price);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }

        protected void PlayBuySound() => 
            _audioSource.PlayOneShot(BuySound);

        protected void PlayErrorSound() => 
            _audioSource.PlayOneShot(ErrorSound);

        protected virtual void UpdatePresenter(uint? value)
        {
            if (value == null)
            {
                PricePresenter.text = "-";
            }
            else
            {
                 PricePresenter.text = value.Value.ToString();
            }
        }

        protected void SpendMoney() =>
            PlayerMoney.TrySpendMoney(UpgradeInfo[NextUpgradeLevel].Price);

        public abstract void OnClick();

        public void LoadProgress(PlayerProgress playerProgress)
        {
            throw new NotImplementedException();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public struct UpgradeInfo
    {
        [SerializeField] public int Value;
        [SerializeField] public uint Price;
    }
}