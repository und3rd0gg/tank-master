using System;
using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    public class CollisionObserver : MonoBehaviour
    {
        public event Action<Collision> CollisionEntered;
        public event Action<Collision> CollisionExitted;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.transform.name);
            CollisionEntered?.Invoke(collision);
        }

        private void OnCollisionExit(Collision other)
        {
            CollisionExitted?.Invoke(other);
        }
    }
}