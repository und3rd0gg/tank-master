using TankMaster.Infrastructure.AssetManagement;

namespace TankMaster.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() {
            _sceneLoader.Load(sceneName: AssetPaths.Scenes.Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel() {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit() { }
    }
}