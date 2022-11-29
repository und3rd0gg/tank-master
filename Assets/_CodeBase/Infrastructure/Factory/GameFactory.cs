using System;
using System.Collections.Generic;
using Cinemachine;
using TankMaster._CodeBase.Infrastructure.AssetManagement;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string MainVirtualCameraTag = "MainVirtualCamera";

        private readonly IAssetProvider _assetProvider;
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<IProgressSaver> ProgressWriters { get; } = new();
        private CinemachineVirtualCamera _virtualCamera;

        public GameObject PlayerGameObject { get; set; }
        public event Action PlayerCreated;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(Vector3 creationPoint)
        {
            PlayerGameObject = InstantiateRegistered(AssetPaths.MainPlayer,
                Vector3.zero,
                Quaternion.Euler(0, 90, 0));
            PlayerCreated?.Invoke();
            return PlayerGameObject;
        }

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