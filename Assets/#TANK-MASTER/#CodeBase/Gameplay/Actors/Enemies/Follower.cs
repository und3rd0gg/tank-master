﻿using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public abstract class Follower : MonoBehaviour
    {
        [SerializeField]protected NavMeshAgent NavMeshAgent;

        public abstract void SetTarget(Transform target);
    }
}