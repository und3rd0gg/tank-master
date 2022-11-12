using TankMaster._CodeBase.Data;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TankMaster._CodeBase.Infrastructure.Services.SaveLoad;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService persistentProgressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.LastLevel);
        }

        public void Exit() { }

        private void LoadProgressOrInitNew()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress() =>
            new PlayerProgress("Main");
    }
}