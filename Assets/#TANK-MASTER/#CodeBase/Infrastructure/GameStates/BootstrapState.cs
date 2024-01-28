using TankMaster._CodeBase.Infrastructure.AssetManagement;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TankMaster._CodeBase.Infrastructure.Services.SaveLoad;
using TankMaster._CodeBase.Infrastructure.Services.YandexGames;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            RegisterServices();
            //RegisterBackgroundChange();
        }

        public void Enter()
        {
            _sceneLoader.Load(sceneName: AssetPaths.Scenes.Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }

        // private void RegisterBackgroundChange() => 
        //     WebApplication.InBackgroundChangeEvent += inBackground => { Time.timeScale = inBackground ? 0 : 1; };

        private void RegisterServices()
        {
            var services = AllServices.Container;
            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            services.RegisterSingle<IGameFactory>(
                new GameFactory(services.Single<IAssetProvider>()));
            services.RegisterSingle<IYandexGamesService>(new YandexGamesService());
            services.RegisterSingle<IInputService>(new TouchInputService());
            services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>()));
            services.RegisterSingle<IAudioService>(services.Single<IGameFactory>().CreateAudioService());
        }
    }
}