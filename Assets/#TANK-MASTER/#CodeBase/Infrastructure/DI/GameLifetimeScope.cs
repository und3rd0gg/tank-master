using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.Infrastructure.Services.SaveLoad;
using TankMaster.Infrastructure.Services.YandexGames;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TankMaster.Infrastructure.DI
{
  public class GameLifetimeScope : LifetimeScope
  {
    [SerializeField] private AudioService _audioService;
    [SerializeField] private NPCFactory _npcFactory;

    protected override void Configure(IContainerBuilder builder) {
      builder
        .Register<AssetProvider>(Lifetime.Singleton)
        .As<IAssetProvider>();

      builder
        .Register<IGameFactory>(resolver =>
            new GameFactory(resolver.Resolve<IAssetProvider>(), resolver.Resolve<IObjectResolver>()),
          Lifetime.Singleton);

      builder
        .RegisterComponent(_npcFactory)
        .AsSelf();

      builder
        .Register<IEnvFactory>(resolver =>
            new EnvFactory(resolver.Resolve<IAssetProvider>(),
              resolver.Resolve<IObjectResolver>(), resolver.Resolve<IGameFactory>(), 
              resolver.Resolve<NPCFactory>()),
          Lifetime.Singleton);

      builder
        .Register<YandexGamesService>(Lifetime.Singleton)
        .As<IYandexGamesService>();

      builder
        .Register(resolver =>
          new TouchInputService(resolver.Resolve<IGameFactory>()), Lifetime.Singleton)
        .As<IInputService>();

      builder
        .Register<PersistentProgressService>(Lifetime.Singleton)
        .As<IPersistentProgressService>();

      builder.Register(resolver =>
          new SaveLoadService(resolver.Resolve<IGameFactory>(),
            resolver.Resolve<IPersistentProgressService>()), Lifetime.Singleton)
        .As<ISaveLoadService>();

      builder
        .RegisterComponentInNewPrefab(_audioService, Lifetime.Singleton)
        .DontDestroyOnLoad()
        .As<IAudioService>();
    }
  }
}