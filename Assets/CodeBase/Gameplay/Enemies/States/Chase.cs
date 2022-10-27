using System;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay.Enemies.States
{
    public class Chase : IPayloadedState<Transform>
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Shooter _shooter;
        private Transform _target;
        private const float StoppingDistance = 5f;

        public Chase(StateMachine stateMachine, NavMeshAgent navMeshAgent, Shooter shooter)
        {
            _navMeshAgent = navMeshAgent;
            _shooter = shooter;
        }

        public void Exit()
        {
        }

        public void Enter(Transform payload)
        {
            _target = payload;
            RepeatShoot();
        }

        public void Tick()
        {
            // if (PlayerNotReached())
            // {
            //     _navMeshAgent.destination = _target.position;
            // }
        }

        private async void RepeatShoot()
        {
            while (true)
            {
                _shooter.Shoot(_target.position);
                await UniTask.Delay(TimeSpan.FromSeconds(2));
            }
        }

        private bool PlayerNotReached()
        {
            return Vector3.Distance(_navMeshAgent.transform.position, _target.position) >= StoppingDistance;
        }
    }
}