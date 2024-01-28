using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Gameplay.Projectiles;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.UI.Panels
{
    public class LosePanel : Panel
    {
        [SerializeField] private Counter _counter;

        public override void Enable()
        {
            base.Enable();
            AllServices.Container.Single<IInputService>().HideVisuals();
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
                var playerGameObject = AllServices.Container.Single<IGameFactory>().PlayerGameObject;
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