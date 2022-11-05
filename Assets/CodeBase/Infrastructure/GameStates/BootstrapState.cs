using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.Infrastructure.Services.SaveLoad;

namespace TankMaster.Infrastructure.GameStates
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
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.Scenes.Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
            //_stateMachine.Enter<LoadLevelState, string>(Constants.Scenes.Main);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            var services = AllServices.Container;
            services.RegisterSingle<IInputService>(new AnalogInputService());
            services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>()));
            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            services.RegisterSingle<IGameFactory>(
                new GameFactory(services.Single<IAssetProvider>()));
        }
    }
}