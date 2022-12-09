using System;
using System.Collections.Generic;
using Cinemachine;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<IProgressSaver> ProgressWriters { get; }
        public GameObject PlayerGameObject { get; }
        public GameObject MainLight { get; }
        public GameObject Interface { get; }
        
        public event Action PlayerCreated;
        public event Action MainLightCreated;

        public GameObject CreatePlayer(Vector3? creationPoint = null);
        public void Cleanup();
        public CinemachineVirtualCamera GetVirtualCamera();
        public void Register(ISavedProgressReader progressReader);
        public void CreateLevelTransition(Vector3 creationPoint, Enemy[] enemiesToEnter);
        public void CreateLevel(Vector3 creationPoint, bool disposePreviousLevel = true);
        public GameObject CreateLight();
        public GameObject CreateInterface();
        public UltimateJoystick CreateJoystick();
        void CreateEventSystem();
        public GameObject CreateMusicSource();
    }
}