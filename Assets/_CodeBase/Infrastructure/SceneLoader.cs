using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TankMaster._CodeBase.Infrastructure
{
    public class SceneLoader
    {
        public void Load(string sceneName, Action onLoaded = null)
        {
            LoadScene(sceneName, onLoaded);
        }

        public async UniTask LoadScene(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                return;
            }
            
            var waitNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (!waitNextScene.isDone)
            {
                await UniTask.Yield();
            }
            
            onLoaded?.Invoke();
        }
    }
}