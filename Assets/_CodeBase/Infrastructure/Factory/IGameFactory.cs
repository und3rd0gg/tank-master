using System;
using System.Collections.Generic;
using Cinemachine;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreatePlayer(Vector3 creationPoint);
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<IProgressSaver> ProgressWriters { get; }
        GameObject PlayerGameObject { get; }
        GameObject MainLight { get; }
        public void Cleanup();
        CinemachineVirtualCamera GetVirtualCamera();
        void Register(ISavedProgressReader progressReader);
        event Action PlayerCreated;
        void CreateLevelTransition(Vector3 creationPoint);
        void CreateLevel(Vector3 creationPoint);
        GameObject CreateLight();
        event Action MainLightCreated;
    }
}