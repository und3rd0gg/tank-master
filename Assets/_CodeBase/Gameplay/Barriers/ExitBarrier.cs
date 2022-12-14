using Agava.YandexGames;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Barriers
{
    public class ExitBarrier : MonoBehaviour
    {
        [SerializeField] private Animator[] _barriersAnimators;
        [SerializeField] private TriggerObserver _triggerOpenObserver;
        [SerializeField] private TriggerObserver _triggerCloseObserver;
        [SerializeField] private GameObject _blocker;

        private static readonly int IsOpened = Animator.StringToHash(nameof(IsOpened));

        private static bool _firstExit = true;

        private void OnEnable()
        {
            _triggerOpenObserver.TriggerEnter += OnPlayerOpenZoneEnter;
            _triggerCloseObserver.TriggerEnter += OnPlayerOpenZoneExit;
        }

        private void OnDisable()
        {
            _triggerOpenObserver.TriggerEnter -= OnPlayerOpenZoneEnter;
            _triggerCloseObserver.TriggerEnter -= OnPlayerOpenZoneExit;
        }

        private void OnPlayerOpenZoneEnter(Collider obj)
        {
            foreach (var barrier in _barriersAnimators)
            {
                barrier.SetBool(IsOpened, true);
                _blocker.SetActive(false);
            }
        }

        private void OnPlayerOpenZoneExit(Collider obj)
        {
            foreach (var barrier in _barriersAnimators)
            {
                barrier.SetBool(IsOpened, false);
                _blocker.SetActive(true);
            }
            
            ShowInterstitial();
        }

        private void ShowInterstitial()
        {
            if (_firstExit)
            {
                _firstExit = false;
                _triggerCloseObserver.Disable();
            }
            else
            {
                InterstitialAd.Show(
                    onOpenCallback: () =>
                    {
                        AllServices.Container.Single<IAudioService>().MuteSound();
                        Time.timeScale = 0;
                    },
                    onCloseCallback: b =>
                    {
                        Time.timeScale = 1;
                        AllServices.Container.Single<IAudioService>().UnmuteSound();
                    });
            }
        }
    }
}