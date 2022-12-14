using System;
using System.Collections.Generic;
using Cinemachine;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Infrastructure.AssetManagement;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TankMaster._CodeBase.Logic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TankMaster._CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string MainVirtualCameraTag = "MainVirtualCamera";
        private const string EnemyTag = "Enemy";
        private const string PlayerInitialPointTag = "PlayerInitialPoint";

        private readonly IAssetProvider _assetProvider;

        private CinemachineVirtualCamera _virtualCamera;
        private GameObject[] _levels;
        private GameObject _transition;

        public List<IProgressSaver> ProgressWriters { get; } = new();
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public GameObject PlayerGameObject { get; private set; }
        public GameObject MainLight { get; private set; }
        public GameObject Interface { get; private set; }

        public event Action PlayerCreated;
        public event Action MainLightCreated;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _levels = assetProvider.LoadAll(AssetPaths.Levels);
            _transition = assetProvider.Load(AssetPaths.Transition);
        }

        public GameObject CreatePlayer(Vector3? creationPoint = null)
        {
            var playerInitialRotation = Quaternion.Euler(0, 90, 0);

            if (!creationPoint.HasValue)
            {
                PlayerGameObject = InstantiateRegistered(AssetPaths.MainPlayer,
                    GameObject.FindGameObjectWithTag(PlayerInitialPointTag).transform.position,
                    playerInitialRotation);
            }
            else
            {
                PlayerGameObject =
                    InstantiateRegistered(AssetPaths.MainPlayer, creationPoint.Value, playerInitialRotation);
            }

            PlayerCreated?.Invoke();
            return PlayerGameObject;
        }

        public GameObject CreateInterface() =>
            Interface = _assetProvider.Instantiate(AssetPaths.Interface, Vector3.zero);

        public UltimateJoystick CreateJoystick() =>
            _assetProvider.Instantiate(AssetPaths.Joystick, Vector3.zero, true)
                .GetComponentInChildren<UltimateJoystick>();

        public GameObject CreateMusicSource()
        {
            return _assetProvider.Instantiate(AssetPaths.Music, Vector3.zero);
        }

        public void CreateLevelTransition(Vector3 creationPoint, Enemy[] enemiesToEnter)
        {
            var transition = _assetProvider.Instantiate(_transition, creationPoint);
            transition.GetComponent<LevelTransition>().EnterBarrier.SetEnterLimitThreshold(enemiesToEnter);
        }

        public void CreateLevel(Vector3 creationPoint, bool disposePreviousLevel = true)
        {
            var level = _assetProvider.Instantiate(GetRandomLevel(), creationPoint);
            var transitionCreationPoint = level.GetComponent<Level>().TransitionConnectionPoint.position;
            var enemiesGameObjects = GameObject.FindGameObjectsWithTag(EnemyTag);
            var enemies = new Enemy[enemiesGameObjects.Length];

            for (var i = 0; i < enemiesGameObjects.Length; i++)
                enemies[i] = enemiesGameObjects[i].GetComponent<Enemy>();

            CreateLevelTransition(transitionCreationPoint, enemies);
        }

        public GameObject CreateMonoService(string path) => 
            _assetProvider.Instantiate(path, Vector3.zero, true);

        public IAudioService CreateAudioService()
        {
            var audioService = CreateMonoService(AssetPaths.AudioService);
            return audioService.GetComponent<AudioService>();
        }

        public GameObject CreateLight()
        {
            MainLight = _assetProvider.Instantiate(AssetPaths.MainLight, Vector3.zero);
            MainLightCreated?.Invoke();
            return MainLight;
        }

        public void CreateEventSystem() => 
            _assetProvider.Instantiate(AssetPaths.EventSystem, Vector3.zero);

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public CinemachineVirtualCamera GetVirtualCamera() =>
            _virtualCamera ??= GameObject.FindWithTag(MainVirtualCameraTag).GetComponent<CinemachineVirtualCamera>();

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject GetRandomLevel() =>
            _levels[Random.Range(0, _levels.Length)];

        private GameObject InstantiateRegistered(string prefabPath, Vector3 creationPoint, Quaternion startRotation)
        {
            var gameObject = _assetProvider.Instantiate(prefabPath, creationPoint, startRotation);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            return InstantiateRegistered(prefabPath, Vector3.zero, Quaternion.identity);
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var reader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(reader);
            }
        }
    }
}