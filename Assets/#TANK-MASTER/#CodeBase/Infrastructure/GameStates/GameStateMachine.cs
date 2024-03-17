using System;
using System.Collections.Generic;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.PersistentProgress;
using TankMaster.Infrastructure.Services.SaveLoad;
using VContainer;

namespace TankMaster.Infrastructure.GameStates
{
    public class GameStateMachine
    {
        private readonly SceneLoader _sceneLoader;
        private readonly Dictionary<Type, IExitableState> _states;
        private readonly IObjectResolver _resolver;

        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, IObjectResolver resolver) {
            _resolver = resolver;
            _sceneLoader = sceneLoader;

            _states = new Dictionary<Type, IExitableState> {
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader),
                [typeof(LoadPlayableLevelState)] = new LoadPlayableLevelState(resolver, this,
                    _sceneLoader, resolver.Resolve<IGameFactory>(),
                    resolver.Resolve<IPersistentProgressService>(), 
                    resolver.Resolve<IEnvFactory>()),
                [typeof(LoadProgressState)] =
                    new LoadProgressState(this,
                        resolver.Resolve<IPersistentProgressService>(),
                        resolver.Resolve<ISaveLoadService>(), _sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(CutsceneState)] = new CutsceneState(this, _sceneLoader),
                [typeof(TutorialState)] = new TutorialState(this, _sceneLoader,
                    _resolver.Resolve<IGameFactory>(), _resolver.Resolve<IInputService>()),
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState GetState<TState>() where TState : class, IExitableState {
            return _states[typeof(TState)] as TState;
        }

        private TState ChangeState<TState>() where TState : class, IExitableState {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}