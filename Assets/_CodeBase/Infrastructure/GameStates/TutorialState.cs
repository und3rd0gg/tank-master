using Cinemachine;
using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.AssetManagement;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class TutorialState : IState
    {
        private const string MainVirtualCameraTag = "MainVirtualCamera";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public TutorialState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }
        
        public void Enter()
        {
            Initialize();
            //todo раскоментить когда будет реализовано
            //_sceneLoader.Load(AssetPaths.Scenes.IntroCutscene);
        }

        public void Exit() { }

        private async void Initialize()
        {
            await _sceneLoader.LoadScene(AssetPaths.Scenes.Tutorial);
            InitGameWorld();
        }
        
        private void InitGameWorld()
        {
            _gameFactory.CreateEventSystem();
            var player = _gameFactory.CreatePlayer();
            AllServices.Container.Single<IInputService>().ShowVisuals();
            _gameFactory.CreateInterface();
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