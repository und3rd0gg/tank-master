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

        [Inject]
        internal void Construct(IInputService inputService, IGameFactory gameFactory) {
            _gameFactory = gameFactory;
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
                var playerGameObject = _gameFactory.PlayerGameObject;
                var playerHealth = playerGameObject.GetComponent<Player>().Health;
                playerHealth.RestoreHealth();
                playerGameObject.SetActive(true);
                _counter.StartCount();
                DestroyAllProjectiles();
                playerHealth.RestoreHealth();
            }

            void DestroyAllProjectiles()
            {
                var proj = FindObjectsOfType<Projectile>();

                foreach (var projectile in proj)
                {
                    Destroy(projectile.gameObject);
                }
            }
        }
    }
}