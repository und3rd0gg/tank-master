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
        private readonly IObjectResolver _objectResolver;
        
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _sceneLoader = sceneLoader;

            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader),
                [typeof(LoadPlayableLevelState)] = new LoadPlayableLevelState(objectResolver,this,
                    _sceneLoader, objectResolver.Resolve<IGameFactory>(),
                    objectResolver.Resolve<IPersistentProgressService>()),
                [typeof(LoadProgressState)] =
                    new LoadProgressState(this, 
                        objectResolver.Resolve<IPersistentProgressService>(),
                        objectResolver.Resolve<ISaveLoadService>(), _sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(CutsceneState)] = new CutsceneState(this, _sceneLoader),
                [typeof(TutorialState)] = new TutorialState(this, _sceneLoader, 
                    _objectResolver.Resolve<IGameFactory>(), _objectResolver.Resolve<IInputService>()),
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