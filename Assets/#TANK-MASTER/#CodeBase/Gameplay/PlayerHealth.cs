using System;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    [Serializable]
    public class PlayerHealth : Health
    {
        [SerializeField] private float _cameraShakeThreshold;
        
        private CameraShaker _cameraShaker;

        public override void ApplyDamage(uint damage)
        {
            ApplyCameraShake(damage);
            base.ApplyDamage(damage);
        }

        private void ApplyCameraShake(uint damage)
        {
            _cameraShaker ??= AllServices.Container.Single<IGameFactory>()
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