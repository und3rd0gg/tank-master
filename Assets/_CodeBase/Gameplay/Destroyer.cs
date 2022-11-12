using Dythervin.AutoAttach;
using Unity.Mathematics;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    [RequireComponent(typeof(Health))]
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destroyVFX;
        [SerializeField] [Attach] private Health _health;

        private void OnEnable()
        {
            _health.Died += HealthOnDied;
        }

        private void OnDisable()
        {
            _health.Died -= HealthOnDied;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Instantiate(_destroyVFX, transform.position, quaternion.identity);
        }

        private void HealthOnDied()
        {
            Destroy();
        }
    }
}