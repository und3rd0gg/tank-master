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
using UnityEngine.Serialization;

namespace TankMaster._CodeBase.UI.Store.Buttons
{
    [RequireComponent(typeof(AudioSource), typeof(UnityEngine.UI.Button))]
    public abstract class StoreItemButton : MonoBehaviour, IProgressSaver
    {
        [SerializeField] [Attach] private AudioSource _audioSource;
        
        [SerializeField] [Attach] protected UnityEngine.UI.Button Button;

        [SerializeField] protected InspectableDictionary<uint, UpgradeInfo> UpgradeInfo;
        [SerializeField] protected TMP_Text PricePresenter;
        
        [SerializeField] private AudioClip _buySound;
        [SerializeField] private AudioClip _errorSound;

        private Money _playerMoney;
        
        protected Money PlayerMoney => _playerMoney ??= AllServices.Container.Single<IGameFactory>().PlayerGameObject
            .GetComponent<Player>()
            .Money;

        protected virtual bool BuyCondition { get; }

        protected virtual void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        protected void PlayBuySound() => 
            PlayIfExists(_buySound);

        protected void PlayErrorSound() => 
            PlayIfExists(_errorSound);

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

        protected void SpendMoney(uint amount) =>
            PlayerMoney.TrySpendMoney(amount);

        private void PlayIfExists(AudioClip audioClip)
        {
            if (audioClip != null) 
                _audioSource.PlayOneShot(audioClip);
        }

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