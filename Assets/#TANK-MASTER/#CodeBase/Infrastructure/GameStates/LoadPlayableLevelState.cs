using Cinemachine;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace TankMaster.Infrastructure.GameStates
{
    public class LoadPlayableLevelState : IPayloadedState<string>
    {
        private const string MainVirtualCameraTag = "MainVirtualCamera";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IObjectResolver _objectResolver;
        private readonly IPersistentProgressService _progressService;
        private IGameFactory _gameFactory;

        public LoadPlayableLevelState(IObjectResolver objectResolver, GameStateMachine stateMachine,
            SceneLoader sceneLoader, IGameFactory gameFactory,
            IPersistentProgressService progressService)
        {
            _objectResolver = objectResolver;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Exit() { }

        public void Enter(string payload)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(payload, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            InitGameWorld().Forget();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.PlayerProgress);
            }
        }

        private async UniTaskVoid InitGameWorld()
        {
            _gameFactory.CreateLevelTransition(Vector3.zero, null);
            _gameFactory.CreateLight();
            _gameFactory.CreateMusicSource();
            _gameFactory.CreateInterface();
            _gameFactory.CreateEventSystem();
            var player = await _gameFactory.CreatePlayer();
            CameraFollow(player);
            _objectResolver.Resolve<IInputService>().ShowVisuals();
        }

        private void CameraFollow(GameObject player)
        {
            var followTarget = player.GetComponentInChildren<Player>().CameraFollowTarget;
            var camera = GameObject.FindWithTag(MainVirtualCameraTag).GetComponent<CinemachineVirtualCamera>();
            camera.enabled = false;
            camera.Follow = followTarget;
            camera.LookAt = followTarget;
            camera.enabled = true;
        }
    }
}