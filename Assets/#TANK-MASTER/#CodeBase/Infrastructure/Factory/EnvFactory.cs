using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Dreamteck.Splines;
using TankMaster.Gameplay.Actors.NPC.Enemies;
using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Logic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace TankMaster.Infrastructure.Factory
{
  public sealed class EnvFactory : IEnvFactory
  {
    private const string EnemyTag = "Enemy";

    private readonly IAssetProvider _assetProvider;
    private readonly IObjectResolver _objectResolver;
    private readonly IGameFactory _gameFactory;

    private IList<GameObject> _levels = new List<GameObject>();
    private GameObject _transition;
    private NPCFactory _npcFactory;

    public SplineComputer Path { get; private set; }
    public LevelTransition Transition { get; private set; }
    public float PathLength { get; private set; }

    public EnvFactory(IAssetProvider assetProvider, IObjectResolver objectResolver,
      IGameFactory gameFactory, NPCFactory npcFactory) {
      LoadAssets().Forget();
      _npcFactory = npcFactory;
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

      var spawners = level.GetComponentsInChildren<EnemySpawnPoint>(true);

      for (int i = 0; i < spawners.Length; i++) {
        _npcFactory.CreateNPC(spawners[i].NpcType, spawners[i].transform.position);
      }

      MergeSpline(location);
      CreateLevelTransition(transitionCreationPoint, enemies);
    }

    private GameObject GetRandomLevel() {
      return _levels[Random.Range(0, _levels.Count)];
    }
  }
}