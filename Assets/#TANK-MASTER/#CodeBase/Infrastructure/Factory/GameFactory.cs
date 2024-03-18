using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Dreamteck.Splines;
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
        private const string PlayerInitialPointTag = "PlayerInitialPoint";

        private readonly IAssetProvider _assetProvider;

        public CinemachineVirtualCamera VirtualCamera { get; private set; }
        public Camera MainCamera { get; private set; }
        private IObjectResolver _objectResolver;

        public List<IProgressSaver> ProgressWriters { get; } = new();
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public GameObject PlayerGameObject { get; private set; }
        public GameObject MainLight { get; private set; }
        public GameObject Interface { get; private set; }

        public GameFactory(IAssetProvider assetProvider, IObjectResolver objectResolver) {
            _objectResolver = objectResolver;
            _assetProvider = assetProvider;
        }

        public async UniTask<GameObject> CreatePlayer() {
            var playerInitialRotation = Quaternion.Euler(0, 90, 0);

            PlayerGameObject = await Instantiate(AssetPaths.MainPlayerID,
                GameObject.FindGameObjectWithTag(PlayerInitialPointTag).transform.position,
                playerInitialRotation);

            return PlayerGameObject;
        }

        public async UniTask<GameObject> CreateUI() {
            GameObject ui = await Instantiate(AssetPaths.InterfaceID, resolve: false);
            Interface = ui;
            ResolveDependencies(ui);
            return ui;
        }

        public async UniTask<UltimateJoystick> CreateJoystick() {
            GameObject gameObject = await _assetProvider.InstantiateAsync(AssetPaths.JoystickID);
            Object.DontDestroyOnLoad(gameObject);
            return gameObject.GetComponentInChildren<UltimateJoystick>();
        }

        public async UniTask<GameObject> CreateMusicSource() {
            GameObject musicSource = await _assetProvider.InstantiateAsync(AssetPaths.MusicID, Vector3.zero);
            return musicSource;
        }

        public async UniTask<GameObject> CreateMonoService(string path) {
            GameObject gameObject = await _assetProvider.InstantiateAsync(path);
            Object.DontDestroyOnLoad(gameObject);
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

        public CinemachineVirtualCamera GetVirtualCamera() {
            return VirtualCamera ??= GameObject
                .FindWithTag(MainVirtualCameraTag)
                .GetComponent<CinemachineVirtualCamera>();
        }

        public Camera GetMainCamera() =>
            MainCamera ??= Camera.main;

        private async UniTask<GameObject> Instantiate(string id, Vector3? pos = null, Quaternion? rot = null,
            Transform parent = null, bool dontDestroyOnLoad = false, bool register = true, bool resolve = true) {
            Debug.Log("включение 1");
            GameObject obj = await _assetProvider.InstantiateAsync(id, pos, rot, parent, enabled: false);
            Debug.Log("включение 2");

            if (register) {
                RegisterProgressWatchers(obj);
            }

            if (resolve) {
                ResolveDependencies(obj);
            }

            if (dontDestroyOnLoad) {
                Object.DontDestroyOnLoad(obj);
            }

            Debug.Log("включение");
            obj.SetActive(true);
            return obj;
        }

        public void ResolveDependencies(GameObject gameObject) {
            MonoBehaviour[] objects = gameObject.GetComponentsInChildren<MonoBehaviour>(true);

            for (var i = 0; i < objects.Length; i++) {
                _objectResolver.Inject(objects[i]);
            }
        }

        private void RegisterProgressWatchers(GameObject gameObject) {
            foreach (ISavedProgressReader reader in gameObject.GetComponentsInChildren<ISavedProgressReader>(true)) {
                Register(reader);
            }
        }

        public void Register(ISavedProgressReader progressReader) {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }

    public interface IEnvFactory
    {
        LevelTransition Transition { get; }
        SplineComputer Path { get; }
        float PathLength { get; }

        void CreateLevelTransition(Vector3 creationPoint, Enemy[] enemiesToEnter);
        void CreateLevel(Vector3 creationPoint, bool disposePreviousLevel = true);
    }

    public sealed class EnvFactory : IEnvFactory
    {
        private const string EnemyTag = "Enemy";

        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly IGameFactory _gameFactory;

        private IList<GameObject> _levels = new List<GameObject>();
        private GameObject _transition;

        public SplineComputer Path { get; private set; }
        public LevelTransition Transition { get; private set; }
        public float PathLength { get; private set; }

        public EnvFactory(IAssetProvider assetProvider, IObjectResolver objectResolver,
            IGameFactory gameFactory) {
            LoadAssets().Forget();
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _gameFactory = gameFactory;
            CreatePath();

            Debug.Assert(_assetProvider != null);
            Debug.Assert(_objectResolver != null);
            Debug.Assert(_gameFactory != null);
        }

        private void CreatePath() {
            Path = new GameObject("PATH").AddComponent<SplineComputer>();
            Path.type = Spline.Type.BSpline;
            Object.DontDestroyOnLoad(Path.gameObject);
        }

        private async UniTaskVoid LoadAssets() {
            GameObject loadedObjects = await Addressables
                .LoadAssetAsync<GameObject>("Assets/#TANK-MASTER/Res/Prefabs/GamePlay/Levels/(LEVEL2).prefab")
                .ToUniTask();
            _levels.Add(loadedObjects);

            _transition = await _assetProvider.Load(AssetPaths.TransitionID);
        }

        public void CreateLevelTransition(Vector3 creationPoint, Enemy[] enemiesToEnter) {
            GameObject transition = _assetProvider.Instantiate(_transition, creationPoint);
            _gameFactory.ResolveDependencies(transition);
            var levelTransition = transition.GetComponent<LevelTransition>();
            levelTransition.EnterBarrier.SetEnterLimitThreshold(enemiesToEnter);
            Transition = levelTransition;
            MergeSpline(levelTransition);
        }

        private void MergeSpline(LevelBase location) {
            var points = location.Path.GetPoints();
            Object.Destroy(location.Path);
            var lastPointIndex = Path.GetPoints().Length;

            int index = lastPointIndex - 1;

            foreach (SplinePoint point in points) {
                Path.SetPoint(++index, point);
            }

            PathLength = Path.CalculateLength();
        }

        public void CreateLevel(Vector3 creationPoint, bool disposePreviousLevel = true) {
            GameObject level = _assetProvider.Instantiate(GetRandomLevel(), creationPoint);
            _gameFactory.ResolveDependencies(level);
            var location = level.GetComponent<Level>();
            Vector3 transitionCreationPoint = location.TransitionConnectionPoint.position;
            GameObject[] enemiesGameObjects = GameObject.FindGameObjectsWithTag(EnemyTag);
            var enemies = new Enemy[enemiesGameObjects.Length];

            for (var i = 0; i < enemiesGameObjects.Length; i++) {
                enemies[i] = enemiesGameObjects[i].GetComponent<Enemy>();
            }

            MergeSpline(location);
            CreateLevelTransition(transitionCreationPoint, enemies);
        }

        private GameObject GetRandomLevel() {
            return _levels[Random.Range(0, _levels.Count)];
        }
    }
}