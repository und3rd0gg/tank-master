using System.Collections.Generic;
using Cinemachine;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreatePlayer(Vector3 creationPoint);
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<IProgressSaver> ProgressWriters { get; }
        public void Cleanup();
        CinemachineVirtualCamera GetVirtualCamera();
    }
}