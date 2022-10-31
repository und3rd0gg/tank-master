using System;
using Cinemachine;
using TankMaster.Gameplay.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using Object = System.Object;

namespace TankMaster.Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            var player = _gameFactory.CreatePlayer(GameObject.FindWithTag("PlayerInitialPoint").transform.position);
            CameraFollow(player);
        }

        private void CameraFollow(GameObject player)
        {
            var followTarget = player.GetComponentInChildren<Player>().CameraFollowTarget;
            var camera = GameObject.FindWithTag("MainVirtualCamera").GetComponent<CinemachineVirtualCamera>();
            camera.Follow = followTarget;
            camera.LookAt = followTarget;
        }
    }
}