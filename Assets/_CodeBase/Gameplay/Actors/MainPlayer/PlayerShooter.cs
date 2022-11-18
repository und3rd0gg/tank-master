using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TankMaster._CodeBase.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    public class PlayerShooter : MonoBehaviour, IAttacker
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _shootDelay;
        [SerializeField] private Transform _shootPoint;

        private Transform _target;
        private CancellationTokenSource _shootCancellationTokenSource;

        public void OnEnable()
        {
            StartShooting();
        }

        private void OnDisable()
        {
            _shootCancellationTokenSource.Cancel();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void StartShooting()
        {
            _shootCancellationTokenSource = new CancellationTokenSource();
            ShootAsync();
        }

        private async UniTask ShootAsync()
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_shootDelay),
                    cancellationToken: _shootCancellationTokenSource.Token);
                Attack();
            }
        }

        private void Attack()
        {
            var shootPoint = _shootPoint.position;
            var proj = Instantiate(_projectile, shootPoint, Quaternion.identity);
            proj.Launch(shootPoint, _target);
        }
    }
}