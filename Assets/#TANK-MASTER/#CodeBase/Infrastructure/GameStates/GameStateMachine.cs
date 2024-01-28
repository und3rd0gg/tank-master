using System;
using System.Collections.Generic;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Infrastructure.Services.PersistentProgress;
using TankMaster._CodeBase.Infrastructure.Services.SaveLoad;

namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public class GameStateMachine
    {
        private readonly SceneLoader _sceneLoader;
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            var services = AllServices.Container;

            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader),
                [typeof(LoadPlayableLevelState)] = new LoadPlayableLevelState(this, _sceneLoader, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>()),
                [typeof(LoadProgressState)] =
                    new LoadProgressState(this, services.Single<IPersistentProgressService>(),
                        services.Single<ISaveLoadService>(), _sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(CutsceneState)] = new CutsceneState(this, _sceneLoader),
                [typeof(TutorialState)] = new TutorialState(this, _sceneLoader),
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}