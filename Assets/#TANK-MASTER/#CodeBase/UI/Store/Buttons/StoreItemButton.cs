using System;
using BuildingBlocks.DataTypes;
using TankMaster.Data;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.Store.Buttons
{
    [RequireComponent(typeof(AudioSource), typeof(UnityEngine.UI.Button))]
    public abstract class StoreItemButton : MonoBehaviour, IProgressSaver
    {
        [SerializeField] private AudioSource _audioSource;
        
        [SerializeField] protected UnityEngine.UI.Button Button;

        [SerializeField] protected InspectableDictionary<uint, UpgradeInfo> UpgradeInfo;
        [SerializeField] protected TMP_Text PricePresenter;
        
        [SerializeField] private AudioClip _buySound;
        [SerializeField] private AudioClip _errorSound;

        private Money _playerMoney;
        
        protected Player Player;

        protected Money PlayerMoney => _playerMoney ??= Player.Money;

        protected virtual bool BuyCondition { get; }

        [Inject]
        internal void Construct(IPlayerService playerService) {
            Player = playerService.GetPlayer();
        }

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