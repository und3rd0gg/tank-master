using System;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
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

        public UniTask<GameObject> CreatePlayer(Vector3? creationPoint = null);
        public void Cleanup();
        public CinemachineVirtualCamera GetVirtualCamera();
        public void Register(ISavedProgressReader progressReader);
        public void CreateLevelTransition(Vector3 creationPoint, Enemy[] enemiesToEnter);
        public void CreateLevel(Vector3 creationPoint, bool disposePreviousLevel = true);
        public UniTask<GameObject> CreateLight();
        public UniTask<GameObject> CreateInterface();
        public UniTask<UltimateJoystick> CreateJoystick();
        void CreateEventSystem();
        public UniTask<GameObject> CreateMusicSource();
        UniTask<IAudioService> CreateAudioService();
    }
}