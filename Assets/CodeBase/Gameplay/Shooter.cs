using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        
        private ShootProfile _shootProfile;
        private IDamageable _target;
        private CancellationTokenSource _shootTasksToken;

        private void Reset()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            RunShootTasks();

            void RunShootTasks()
            {
                _shootTasksToken = new();
                var usedProjectiles = _shootProfile.ProjectileInfo;

                foreach (var projectile in usedProjectiles)
                {
                    RepeatShootAsync(projectile, _shootTasksToken.Token);
                }
            }
        }

        private void OnDisable()
        {
            StopShootTasks();

            void StopShootTasks() => 
                _shootTasksToken.Cancel();
        }

        public void SetShootProfile(ShootProfile shootProfile) =>
            _shootProfile = shootProfile;

        public void SetTarget(IDamageable target) =>
            _target = target;

        private void Shoot(Transform target, Projectile projectile)
        {
            var position = _shootPoint.position;
            var proj = Instantiate(projectile, position, Quaternion.identity);
            proj.Launch(position, target);
        }
        
        private async UniTask RepeatShootAsync(ProjectileInfo projectileInfo, CancellationToken cancellationToken)
        {
            while (true)
            {
                Shoot(_target.transform, projectileInfo.Projectile);
                await UniTask.Delay(TimeSpan.FromSeconds(projectileInfo.Delay),
                    cancellationToken: cancellationToken);
            }
        }
    }
}