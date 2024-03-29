using DG.Tweening;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TMPro;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.HUD
{
    public class BalancePresenter : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _openTime;
        [SerializeField] private float _showDelay;

        private Money _money;
        private Coroutine _showRoutine;
        private bool _timerStarted;
        private float _timer;
        private Player _player;

        [Inject]
        internal void Construct(IPlayerService playerService) {
            _player = playerService.GetPlayer();
        }

        private void Start()
        {
            // if (_gameFactory.PlayerGameObject != null)
            //     InitializeMoney();
            // else
            //     _gameFactory.PlayerCreated += GameFactoryOnPlayerCreated;
        }

        private void Update()
        {
            RunCloseDelayTimer();
        }

        public void Open()
        {
            _rectTransform
                .DOAnchorPosX(0, _openTime)
                .SetUpdate(UpdateType.Normal, true);
        }

        public void Close()
        {
            _rectTransform
                .DOAnchorPosX(300, _openTime)
                .SetUpdate(UpdateType.Normal, true);
        }

        private void RunCloseDelayTimer()
        {
            if (!_timerStarted) return;

            _timer -= Time.deltaTime;

            if (!(_timer <= 0)) return;

            Close();
            _timerStarted = false;
        }

        private void InitializeMoney()
        {
            _money = _player.Money;
            _money.ValueChanged += MoneyOnValueChanged;
        }

        private void GameFactoryOnPlayerCreated()
        {
            // InitializeMoney();
            // _gameFactory.PlayerCreated -= GameFactoryOnPlayerCreated;
        }

        private void MoneyOnValueChanged(uint currentValue, uint maxValue)
        {
            _text.text = currentValue.ToString();
            _timer = _showDelay;

            if (_timerStarted) return;

            _timerStarted = true;
            Open();
        }
    }
}