using System;
using System.Collections.Generic;
using Dythervin.AutoAttach;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay.Actors.Enemies.States
{
    public class EnemyStateMachine : ActorStateMachine
    {
        [SerializeField] [Attach(Attach.Child)]
        private Detector _detector;

        [SerializeField] [Attach] private Enemy _enemy;
        [SerializeField] [Attach] private NavMeshAgent _navMeshAgent;
        [SerializeField] [Attach] private Shooter _shooter;

        private ITickableState _activeState;

        protected override void InitializeStates()
        {
            States = new Dictionary<Type, ITickableState>
            {
                [typeof(Idle)] = new Idle(this, _detector),
                [typeof(ChaseAndAttack)] =
                    new ChaseAndAttack(this, _enemy.EnemyProfile, _navMeshAgent, _shooter, _detector),
            };
        }

        protected override void SetDefaultState()
        {
            Enter<Idle>();
        }
    }
}