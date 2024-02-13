using TankMaster.Data;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace TankMaster.Infrastructure.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
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
            LaunchCutscene();
        }

        private void LaunchCutscene()
        {
            _stateMachine.Enter<CutsceneState>();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
        }
    }
}