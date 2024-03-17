using System;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.Logic;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }
        GameObject PlayerGameObject { get; }
        GameObject MainLight { get; }
        GameObject Interface { get; }

        UniTask<GameObject> CreatePlayer();
        void Cleanup();
        CinemachineVirtualCamera GetVirtualCamera();
        void Register(ISavedProgressReader progressReader);
        UniTask<GameObject> CreateLight();
        UniTask<GameObject> CreateUI();
        UniTask<UltimateJoystick> CreateJoystick();
        void CreateEventSystem();
        public UniTask<GameObject> CreateMusicSource();
        UniTask<IAudioService> CreateAudioService();
        void ResolveDependencies(GameObject gameObject);
        Camera GetMainCamera();
    }
}