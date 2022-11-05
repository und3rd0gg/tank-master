using System.Collections.Generic;
using Cinemachine;
using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string MainVirtualCameraTag = "MainVirtualCamera";
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<IProgressSaver> ProgressWriters { get; } = new();

        private CinemachineVirtualCamera _virtualCamera;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(Vector3 creationPoint)
        {
            return _assetProvider.Instantiate(AssetPaths.MainPlayer, creationPoint, Quaternion.Euler(0, 90, 0));
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public CinemachineVirtualCamera GetVirtualCamera() => 
            _virtualCamera ??= GameObject.FindWithTag(MainVirtualCameraTag).GetComponent<CinemachineVirtualCamera>();

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var reader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(reader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}