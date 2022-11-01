using System;
using System.Collections.Generic;
using Dythervin.AutoAttach;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay.Enemies.States
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] [Attach(Attach.Child)]
        private Detector _detector;
        [SerializeField] [Attach] private Enemy _enemy;
        [SerializeField] [Attach] private NavMeshAgent _navMeshAgent;
        [SerializeField] [Attach] private Shooter _shooter;

        private Dictionary<Type, ITickableState> _states;
        private ITickableState _activeState;

        private void Awake()
        {
            InitializeStates();
            Enter<Idle>();

            void InitializeStates()
            {
                _states = new Dictionary<Type, ITickableState>
                {
                    [typeof(Idle)] = new Idle(this, _detector),
                    [typeof(ChaseAndAttack)] = new ChaseAndAttack(this, _enemy.EnemyProfile, _navMeshAgent, _shooter, _detector),
                };
            }
        }

        private void Update()
        {
            _activeState?.Tick();
        }

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
            return _states[typeof(TState)] as TState;
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