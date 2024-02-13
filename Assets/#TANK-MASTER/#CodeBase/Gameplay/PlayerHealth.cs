using System;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.Gameplay
{
    [Serializable]
    public class PlayerHealth : Health
    {
        [SerializeField] private float _cameraShakeThreshold;
        
        private CameraShaker _cameraShaker;
        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }

        public override void ApplyDamage(uint damage)
        {
            ApplyCameraShake(damage);
            base.ApplyDamage(damage);
        }

        private void ApplyCameraShake(uint damage)
        {
            _cameraShaker ??= _gameFactory
                .GetVirtualCamera()
                .GetComponent<CameraShaker>();
            var damagePercentage = GetDamagePercentage(damage, MaxValue);

            if (damagePercentage >= _cameraShakeThreshold)
            {
                _cameraShaker.ShakeCamera(duration: 0.3f);
            }
        }

        private float GetDamagePercentage(uint damage, uint maxValue)
        {
            return (float) damage / maxValue * 100;
        }
    }
}