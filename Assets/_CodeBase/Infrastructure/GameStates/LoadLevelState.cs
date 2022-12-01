using System;
using Cinemachine;
using TankMaster._CodeBase.Gameplay.Actors.MainPlayer;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitPointTag = "PlayerInitialPoint";
        private const string MainVirtualCameraTag = "MainVirtualCamera";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IPersistentProgressService progressService)
        {
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
            InitGameWorld();
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

        private void InitGameWorld()
        {
            _gameFactory.CreateLevelTransition(Vector3.zero);
            _gameFactory.CreateLight();
            var player = _gameFactory.CreatePlayer(GameObject.FindWithTag(PlayerInitPointTag).transform.position);
            CameraFollow(player);
            //InitSpawners();
        }

        // private void InitSpawners()
        // {
        //     foreach (var gameObject in GameObject.FindGameObjectsWithTag("EnemySpawner"))
        //     {
        //         var spawner = gameObject.GetComponent<EnemySpawner>();
        //         _gameFactory.Register(spawner);
        //     }
        // }

        private void CameraFollow(GameObject player)
        {
            var followTarget = player.GetComponentInChildren<Player>().CameraFollowTarget;
            var camera = GameObject.FindWithTag(MainVirtualCameraTag).GetComponent<CinemachineVirtualCamera>();
            camera.Follow = followTarget;
            camera.LookAt = followTarget;
        }
    }
}