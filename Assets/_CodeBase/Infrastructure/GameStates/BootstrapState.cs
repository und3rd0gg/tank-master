using TankMaster._CodeBase.Infrastructure.AssetManagement;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TankMaster._CodeBase.Infrastructure.Services.SaveLoad;
using TankMaster._CodeBase.Infrastructure.Services.YandexGames;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(sceneName: InitialSceneName, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit() { }

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
        }
    }
}