using TankMaster._CodeBase.Test;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        [SerializeField] private YandexGamesTest _yandexGamesTest;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                Instantiate(_gameBootstrapper);
            }

#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            _yandexGamesTest.Init();
        }
    }
}