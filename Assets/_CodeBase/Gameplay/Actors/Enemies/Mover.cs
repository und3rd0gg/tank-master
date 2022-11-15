using System;
using Dythervin.AutoAttach;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class Mover : MonoBehaviour
    {
        [SerializeField][Attach] private NavMeshAgent _navMeshAgent;

        private Transform _target;

        public float StoppingDistance => 
            _navMeshAgent.stoppingDistance;

        public void Stop() =>
            _navMeshAgent.isStopped = true;

        public void SetTarget(Transform target) => 
            _target = target;

        public void MoveTo(Transform target)
        {
            SetTarget(target);
            _navMeshAgent.destination = target.position;
            _navMeshAgent.isStopped = false;
        }

        public bool TargetNotReached() =>
            Vector3.Distance(_navMeshAgent.transform.position, _target.transform.position) >=
            _navMeshAgent.stoppingDistance;

        public void RotateToTarget(Transform target) => 
            transform.LookAt(target);
    }
}