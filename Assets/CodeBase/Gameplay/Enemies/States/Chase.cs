using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay.Enemies.States
{
    public class Chase : IPayloadedState<IDamageable>
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Shooter _shooter;
        private IDamageable _target;
        private const int ShootDelay = 2;
        private const float StoppingDistance = 5f;

        public Chase(StateMachine stateMachine, NavMeshAgent navMeshAgent, Shooter shooter)
        {
            _navMeshAgent = navMeshAgent;
            _shooter = shooter;
        }

        public void Enter(IDamageable payload)
        {
            _target = payload;
            RepeatShootAsync();
        }

        public void Exit()
        { }

        public void Tick()
        {
            if (PlayerNotReached())
            {
                _navMeshAgent.destination = _target.transform.position;
            }
        }

        private bool PlayerNotReached() =>
            Vector3.Distance(_navMeshAgent.transform.position, _target.transform.position) >= StoppingDistance;

        private async void RepeatShootAsync()
        {
            while (true)
            {
                _shooter.Shoot(_target.transform);
                await UniTask.Delay(TimeSpan.FromSeconds(ShootDelay));
            }
        }
        
        
    }
}