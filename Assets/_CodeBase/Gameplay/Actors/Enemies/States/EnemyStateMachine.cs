using System;
using System.Collections.Generic;
using AYellowpaper;
using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies.States
{
    public class EnemyStateMachine : ActorStateMachine
    {
        [SerializeField] [Attach(Attach.Child)]
        private Detector _detector;

        [SerializeField] [Attach] private Enemy _enemy;
        [SerializeField] [Attach] private Mover _mover;
        [SerializeField] private InterfaceReference<IAttacker> _attacker;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private ITickableState _activeState;

        protected override void InitializeStates()
        {
            States = new Dictionary<Type, ITickableState>
            {
                [typeof(IdleState)] = new IdleState(this, _detector),
                [typeof(ChaseState)] = new ChaseState(this, _enemyAnimator, _enemy.EnemyProfile, _mover, _detector),
                [typeof(AttackState)] = new AttackState(this, _enemyAnimator, _enemy.EnemyProfile, _attacker.Value, _mover,
                    _detector),
            };
        }

        protected override void SetDefaultState()
        {
            Enter<IdleState>();
        }
    }
}