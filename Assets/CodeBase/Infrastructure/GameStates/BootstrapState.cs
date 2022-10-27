using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;

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
            _stateMachine.Enter<LoadLevelState, string>(Constants.Scenes.Main);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            AllServices.Container.RegisterSingle<IInputService>(new AnalogInputService());
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            AllServices.Container.RegisterSingle<IGameFactory>(
                new GameFactory(AllServices.Container.Single<IAssetProvider>()));
        }
    }
}