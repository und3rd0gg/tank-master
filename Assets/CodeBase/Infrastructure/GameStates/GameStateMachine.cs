using System;
using System.Collections.Generic;

namespace TankMaster.Infrastructure.GameStates
{
    public class GameStateMachine
    {
        private readonly SceneLoader _sceneLoader;
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, _sceneLoader),
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