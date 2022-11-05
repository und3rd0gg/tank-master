using System;
using System.Collections.Generic;
using TankMaster.Gameplay.Actors.Enemies.States;
using UnityEngine;

namespace TankMaster.Gameplay.Actors
{
    public abstract class ActorStateMachine : MonoBehaviour
    {
        private ITickableState _activeState;
        
        protected Dictionary<Type, ITickableState> States;

        private void Awake()
        {
            InitializeStates();
            SetDefaultState();
        }

        private void Update()
        {
            _activeState?.Tick();
        }

        protected abstract void InitializeStates();

        protected abstract void SetDefaultState();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IDefaultState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState GetState<TState>() where TState : class, ITickableState
        {
            return States[typeof(TState)] as TState;
        }

        private TState ChangeState<TState>() where TState : class, ITickableState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}