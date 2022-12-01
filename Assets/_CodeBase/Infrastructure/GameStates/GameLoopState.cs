using TankMaster._CodeBase.Infrastructure.AssetManagement;
using Unity.Mathematics;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        { }
    }

    public class WorldLoader
    {
        private GameObject[] _levels;
        private GameObject _transition;

        public WorldLoader()
        {
            LoadResources();
            GameObject.Instantiate(_transition, Vector3.zero, quaternion.identity);
        }

        private void LoadResources()
        {
            _levels = Resources.LoadAll<GameObject>(AssetPaths.Levels);
            _transition = Resources.Load<GameObject>(AssetPaths.Transition);
        }
    }
}