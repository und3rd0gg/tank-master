using System;
using Cinemachine;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.Logic;
using UnityEngine;

namespace TankMaster.Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
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

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Enter(string payload)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(payload, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
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
            var player = _gameFactory.CreatePlayer(GameObject.FindWithTag("PlayerInitialPoint").transform.position);
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
            var camera = GameObject.FindWithTag("MainVirtualCamera").GetComponent<CinemachineVirtualCamera>();
            camera.Follow = followTarget;
            camera.LookAt = followTarget;
        }
    }
}