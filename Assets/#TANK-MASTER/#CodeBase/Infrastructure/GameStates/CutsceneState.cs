using Cysharp.Threading.Tasks;
using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Logic.FirstStartCutscene;
using UnityEngine;

namespace TankMaster.Infrastructure.GameStates
{
    public class CutsceneState : IState
    {
        private const string CutsceneHandler = nameof(CutsceneHandler);
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private CutscenePlayableDirector _cutscenePlayableDirector;

        public CutsceneState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            Initialize().Forget();
        }

        public void Exit() { }

        private async UniTaskVoid Initialize()
        {
            await _sceneLoader.LoadScene(AssetPaths.Scenes.IntroCutscene);
            _cutscenePlayableDirector =
                GameObject.FindWithTag(CutsceneHandler).GetComponent<CutscenePlayableDirector>();
            _cutscenePlayableDirector.CutsceneFinished += OnCutsceneFinished;
        }

        private void OnCutsceneFinished()
        {
            _cutscenePlayableDirector.CutsceneFinished -= OnCutsceneFinished;
            _stateMachine.Enter<LoadPlayableLevelState, string>(AssetPaths.Scenes.Main);
        }
    }
}