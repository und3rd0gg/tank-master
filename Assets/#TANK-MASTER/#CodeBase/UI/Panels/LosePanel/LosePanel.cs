using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Gameplay.Projectiles;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.Panels.LosePanel
{
    public class LosePanel : Panel
    {
        [SerializeField] private Counter _counter;
        private IInputService _inputService;
        private IGameFactory _gameFactory;
        private Player _player;

        [Inject]
        internal void Construct(IInputService inputService, IPlayerService playerService) {
            _player = playerService.GetPlayer();
            _inputService = inputService;
        }

        public override void Enable()
        {
            base.Enable();
            _inputService.HideVisuals();
        }

        public void RestartLevel()
        {
            //InterstitialAd.Show();
            Debug.Log("Restart");
        }

        public void ReviveAfterAd()
        {
            // VideoAd.Show(
            //     onOpenCallback: () =>
            //     {
            //         AllServices.Container.Single<IAudioService>().MuteSound();
            //         Time.timeScale = 0;
            //     }, onCloseCallback: () =>
            //     {
            //         AllServices.Container.Single<IAudioService>().UnmuteSound();
            //         RevivePlayer();
            //     });

            void RevivePlayer()
            {
                gameObject.SetActive(false);
                var playerHealth =  _player.Health;
                playerHealth.RestoreHealth();
                _player.gameObject.SetActive(true);
                _counter.StartCount();
                DestroyAllProjectiles();
                playerHealth.RestoreHealth();
            }

            void DestroyAllProjectiles()
            {
                var proj = FindObjectsOfType<ProjectileBase>();

                foreach (var projectile in proj)
                {
                    Destroy(projectile.gameObject);
                }
            }
        }
    }
}