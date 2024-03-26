using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class BulletShooter : MonoBehaviour, IAttacker
    {
        [SerializeField] private ProjectileBase _projectile;
        [SerializeField] private float _shootDelay;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] [Min(0)] private float _yOffset;
        [SerializeField] private int _bulletsCount;
        [SerializeField] private float _bulletOffset;
        
        [field: SerializeField]public bool ShouldShoot { get; set; }

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

        public void UpgradeBulletCount(int newCount) =>
            _bulletsCount = newCount;

        public void UpgradeShootDelay(float newDelay) => 
            _shootDelay = newDelay;

        private void StartShooting()
        {
            _shootCancellationTokenSource = new CancellationTokenSource();
            ShootAsync().Forget();
        }

        private async UniTask ShootAsync()
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_shootDelay),
                    cancellationToken: _shootCancellationTokenSource.Token);
                if(ShouldShoot)
                    Attack();
            }
        }

        private void Attack()
        {
            var target = _target.position;
            target.y += _yOffset;

            for (int i = 0; i < _bulletsCount; i++)
            {
                foreach (var shootPoint in GetShootPoints(_bulletsCount))
                {
                    var proj = Instantiate(_projectile, shootPoint, Quaternion.identity);
                    proj.Launch(shootPoint, target);
                }
            }
        }

        private Vector3[] GetShootPoints(int pointsCount)
        {
            var points = new Vector3[pointsCount];
            var start = _shootPoint.position - transform.right * _bulletOffset * _bulletsCount / 2;

            for (var i = 1; i <= pointsCount; i++)
            {
                points[i - 1] = start + transform.right * _bulletOffset * i;
            }

            return points;
        }
    }
}