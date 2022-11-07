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
        [SerializeField] [Attach] private Mover _mover;
        [SerializeField] [Attach] private Shooter _shooter;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private ITickableState _activeState;

        protected override void InitializeStates()
        {
            States = new Dictionary<Type, ITickableState>
            {
                [typeof(Idle)] = new Idle(this, _detector),
                [typeof(Chase)] = new Chase(this, _enemyAnimator, _enemy.EnemyProfile, _mover, _detector),
                [typeof(Attack)] = new Attack(this, _enemyAnimator, _enemy.EnemyProfile, _shooter, _mover, _detector),
            };
        }

        protected override void SetDefaultState()
        {
            Enter<Idle>();
        }
    }
}