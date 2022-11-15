using AYellowpaper;
using Dythervin.AutoAttach;
using Unity.Mathematics;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    [RequireComponent(typeof(Health))]
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IActor> _actor;
        [SerializeField] private ParticleSystem _destroyVFX;

        private void OnEnable()
        {
            _actor.Value.Health.Died += OnDied;
        }

        private void OnDisable()
        {
            _actor.Value.Health.Died -= OnDied;
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Instantiate(_destroyVFX, transform.position, quaternion.identity);
        }

        private void OnDied() => 
            Destroy();
    }
}