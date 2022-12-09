using TankMaster._CodeBase.Data;
using TankMaster._CodeBase.Infrastructure.AssetManagement;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TankMaster._CodeBase.Infrastructure.Services.SaveLoad;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly SceneLoader _sceneLoader;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService persistentProgressService,
            ISaveLoadService saveLoadService, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _progressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            LoadRequiredScene();
        }

        private void LoadRequiredScene()
        {
            // if (true)
                 _stateMachine.Enter<CutsceneState>();
            // else
            // {
            //    _stateMachine.Enter<LoadPlayableLevelState, string>(AssetPaths.Scenes.Main);
            //}
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress() =>
            new PlayerProgress();
    }
}