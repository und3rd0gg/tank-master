using System;
using System.Collections.Generic;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Gameplay.Actors.Enemies.States;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer.States
{
    public class MainPlayerStateMachine : ActorStateMachine
    {
        [SerializeField] private TurretRotator _turretRotator;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Detector _detector;

        protected override void InitializeStates()
        {
            States = new Dictionary<Type, ITickableState>
            {
                [typeof(IdleState)] = new IdleState(this, _detector, _turretRotator),
                [typeof(AttackState)] = new AttackState(this, _shooter, _detector),
            };
        }

        protected override void SetDefaultState()
        {
            Enter<IdleState>();
        }
    }
}