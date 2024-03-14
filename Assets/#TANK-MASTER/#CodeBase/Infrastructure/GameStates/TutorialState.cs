using Cinemachine;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;

namespace TankMaster.Infrastructure.GameStates
{
    public class TutorialState : IState
    {
        private const string MainVirtualCameraTag = "MainVirtualCamera";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private IInputService _inputService;

        public TutorialState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IInputService inputService)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            Initialize().Forget();
            //todo раскоментить когда будет реализовано
            //_sceneLoader.Load(AssetPaths.Scenes.IntroCutscene);
        }

        public void Exit() { }

        private async UniTaskVoid Initialize()
        {
            await _sceneLoader.LoadScene(AssetPaths.Scenes.Tutorial);
            InitGameWorld().Forget();
        }
        
        private async UniTaskVoid InitGameWorld()
        {
            _gameFactory.CreateEventSystem();
            _inputService.ShowVisuals();
            _gameFactory.CreateUI();
            var player = await _gameFactory.CreatePlayer();
            CameraFollow(player);
        }
        
        private void CameraFollow(GameObject player)
        {
            //todo убрать дубляж в loadlevelstate
            var followTarget = player.GetComponentInChildren<Player>().CameraFollowTarget;
            var camera = GameObject.FindWithTag(MainVirtualCameraTag).GetComponent<CinemachineVirtualCamera>();
            camera.enabled = false;
            camera.Follow = followTarget;
            camera.LookAt = followTarget;
            camera.enabled = true;
        }
    }
}