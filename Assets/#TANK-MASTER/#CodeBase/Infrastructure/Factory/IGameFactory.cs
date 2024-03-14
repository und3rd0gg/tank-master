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
        LevelTransition Transition { get; }

        UniTask<GameObject> CreatePlayer(Vector3? creationPoint = null);
        void Cleanup();
        CinemachineVirtualCamera GetVirtualCamera();
        void Register(ISavedProgressReader progressReader);
        void CreateLevelTransition(Vector3 creationPoint, Enemy[] enemiesToEnter);
        void CreateLevel(Vector3 creationPoint, bool disposePreviousLevel = true);
        UniTask<GameObject> CreateLight();
        UniTask<GameObject> CreateUI();
        UniTask<UltimateJoystick> CreateJoystick();
        void CreateEventSystem();
        public UniTask<GameObject> CreateMusicSource();
        UniTask<IAudioService> CreateAudioService();
    }
}