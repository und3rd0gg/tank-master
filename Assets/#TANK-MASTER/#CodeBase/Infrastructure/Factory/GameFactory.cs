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
using VContainer;

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
      GameObject ui = await Instantiate(AssetPaths.InterfaceID, resolve: false, enable: false);
      Interface = ui;
      ResolveDependencies(ui);
      Interface.SetActive(true);
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

    public async UniTask<GameObject> Instantiate(string id, Vector3? pos = null, Quaternion? rot = null,
      Transform parent = null, bool dontDestroyOnLoad = false, bool register = true, bool resolve = true,
      bool enable = true) {
      GameObject obj = await _assetProvider.InstantiateAsync(id, pos, rot, parent, enabled: false);
      PostInstantiate(obj, register, resolve, dontDestroyOnLoad);
      obj.SetActive(enable);
      return obj;
    }

    public T Instantiate<T>(T prefab, Vector3? pos = null, Quaternion? rot = null,
      Transform parent = null, bool dontDestroyOnLoad = false, bool register = true, bool resolve = true,
      bool enable = true) where T : Component {
      var obj = _assetProvider.Instantiate(prefab, pos, rot, parent);
      PostInstantiate(obj.gameObject, register, resolve, dontDestroyOnLoad);
      obj.gameObject.SetActive(enable);
      return obj;
    }

    public GameObject Instantiate(GameObject prefab, Vector3? pos = null, Quaternion? rot = null,
      Transform parent = null, bool dontDestroyOnLoad = false, bool register = true, bool resolve = true,
      bool enable = true) {
      var obj = _assetProvider.Instantiate(prefab, pos, rot, parent);
      PostInstantiate(obj, register, resolve, dontDestroyOnLoad);
      obj.SetActive(enable);
      return obj;
    }
    
    private void PostInstantiate(GameObject obj, bool register, bool resolve, bool dontDestroyOnLoad) {
      if (register) {
        RegisterProgressWatchers(obj);
      }

      if (resolve) {
        ResolveDependencies(obj);
      }

      if (dontDestroyOnLoad) {
        Object.DontDestroyOnLoad(obj);
      }
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
}