using System;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.Logic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using Random = UnityEngine.Random;

namespace TankMaster.Infrastructure.Factory
{
    public sealed class GameFactory : IGameFactory
    {
        private const string MainVirtualCameraTag = "MainVirtualCamera";
        private const string EnemyTag = "Enemy";
        private const string PlayerInitialPointTag = "PlayerInitialPoint";

        private readonly IAssetProvider _assetProvider;

        private CinemachineVirtualCamera _virtualCamera;
        private IList<GameObject> _levels = new List<GameObject>();
        private GameObject _transition;
        private IObjectResolver _objectResolver;

        public List<IProgressSaver> ProgressWriters { get; } = new();
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public GameObject PlayerGameObject { get; private set; }
        public GameObject MainLight { get; private set; }
        public GameObject Interface { get; private set; }
        
        public LevelTransition Transition { get; private set; }

        public GameFactory(IAssetProvider assetProvider, IObjectResolver objectResolver) {
            _objectResolver = objectResolver;
            _assetProvider = assetProvider;
            LoadAssets().Forget();
        }

        private async UniTaskVoid LoadAssets() {
            GameObject loadedObjects = await Addressables
                .LoadAssetAsync<GameObject>("Assets/#TANK-MASTER/Res/Prefabs/GamePlay/Levels/(LEVEL2).prefab")
                .ToUniTask();
            _levels.Add(loadedObjects);

            _transition = await _assetProvider.Load(AssetPaths.TransitionID);
        }

        public async UniTask<GameObject> CreatePlayer(Vector3? creationPoint = null) {
            var playerInitialRotation = Quaternion.Euler(0, 90, 0);

            // if (!creationPoint.HasValue) {
            //     PlayerGameObject = await InstantiateRegistered(AssetPaths.MainPlayerID,
            //         GameObject.FindGameObjectWithTag(PlayerInitialPointTag).transform.position,
            //         playerInitialRotation);
            // } else {
            //     PlayerGameObject =
            //         await InstantiateRegistered(AssetPaths.MainPlayerID, creationPoint.Value, playerInitialRotation);
            // }

            PlayerGameObject = await InstantiateRegistered(AssetPaths.MainPlayerID,
                GameObject.FindGameObjectWithTag(PlayerInitialPointTag).transform.position,
                playerInitialRotation, false);

            ResolveDependencies(PlayerGameObject);
            PlayerGameObject.SetActive(true);
            
            return PlayerGameObject;
        }

        public async UniTask<GameObject> CreateUI() {
            GameObject ui = await InstantiateAndInject(AssetPaths.InterfaceID);
            return ui;
        }

        public async UniTask<UltimateJoystick> CreateJoystick() {
            GameObject gameObject = await _assetProvider.InstantiateAsync(AssetPaths.JoystickID,
                dontDestroyOnLoad: true);
            return gameObject.GetComponentInChildren<UltimateJoystick>();
        }

        public async UniTask<GameObject> CreateMusicSource() {
            GameObject musicSource = await _assetProvider.InstantiateAsync(AssetPaths.MusicID, Vector3.zero);
            return musicSource;
        }

        public void CreateLevelTransition(Vector3 creationPoint, Enemy[] enemiesToEnter) {
            GameObject transition = _assetProvider.Instantiate(_transition, creationPoint);
            ResolveDependencies(transition);
            var levelTransition = transition.GetComponent<LevelTransition>();
            levelTransition.EnterBarrier.SetEnterLimitThreshold(enemiesToEnter);
            Transition = levelTransition;
        }

        public void CreateLevel(Vector3 creationPoint, bool disposePreviousLevel = true) {
            GameObject level = _assetProvider.Instantiate(GetRandomLevel(), creationPoint);
            ResolveDependencies(level);
            Vector3 transitionCreationPoint = level.GetComponent<Level>().TransitionConnectionPoint.position;
            GameObject[] enemiesGameObjects = GameObject.FindGameObjectsWithTag(EnemyTag);
            var enemies = new Enemy[enemiesGameObjects.Length];

            for (var i = 0; i < enemiesGameObjects.Length; i++)
                enemies[i] = enemiesGameObjects[i].GetComponent<Enemy>();

            CreateLevelTransition(transitionCreationPoint, enemies);
        }

        public async UniTask<GameObject> CreateMonoService(string path) {
            GameObject gameObject = await _assetProvider.InstantiateAsync(path, dontDestroyOnLoad: true);
            return gameObject;
        }

        public async UniTask<IAudioService> CreateAudioService() {
            GameObject audioService = await CreateMonoService(AssetPaths.AudioServiceID);
            return audioService.GetComponent<AudioService>();
        }

        public async UniTask<GameObject> CreateLight() {
            MainLight = await _assetProvider.InstantiateAsync(AssetPaths.MainLightID, Vector3.zero);
            return MainLight;
        }

        public void CreateEventSystem() {
            _assetProvider.InstantiateAsync(AssetPaths.EventSystemID, Vector3.zero);
        }

        public void Cleanup() {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public CinemachineVirtualCamera GetVirtualCamera() =>
            _virtualCamera ??= GameObject
                .FindWithTag(MainVirtualCameraTag)
                .GetComponent<CinemachineVirtualCamera>();

        public void Register(ISavedProgressReader progressReader) {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject GetRandomLevel() {
            return _levels[Random.Range(0, _levels.Count)];
        }

        private async UniTask<GameObject> InstantiateRegistered(string prefabPath, Vector3 creationPoint,
            Quaternion startRotation, bool isEnabled = true) {
            var gameObject =
                await _assetProvider.InstantiateAsync(prefabPath, creationPoint, startRotation, enabled: isEnabled);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private async UniTask<GameObject> InstantiateAndInject(string id) {
            GameObject obj = await _assetProvider.InstantiateAsync(id);
            ResolveDependencies(obj);
            obj.SetActive(true);
            return obj;
        }

        private void ResolveDependencies(GameObject gameObject) {
            MonoBehaviour[] objects = gameObject.GetComponentsInChildren<MonoBehaviour>(true);

            for (var i = 0; i < objects.Length; i++) {
                _objectResolver.Inject(objects[i]);
            }
        }

        private async UniTask<GameObject> InstantiateRegistered(string prefabPath) {
            return await InstantiateRegistered(prefabPath, Vector3.zero, Quaternion.identity);
        }

        private void RegisterProgressWatchers(GameObject gameObject) {
            foreach (ISavedProgressReader reader in gameObject.GetComponentsInChildren<ISavedProgressReader>()) {
                Register(reader);
            }
        }
    }
}