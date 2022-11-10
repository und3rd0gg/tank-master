using System;
using System.Collections.Generic;
using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Gameplay.Actors.Enemies.States;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer.States
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
                [typeof(Idle)] = new Idle(this, _detector, _turretRotator),
                [typeof(Attack)] = new Attack(this, _shooter, _detector),
            };
        }

        protected override void SetDefaultState()
        {
            Enter<Idle>();
        }
    }
}