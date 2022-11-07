using System.Threading;
using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private ShootPoints _shootPoints;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private ShootProfile _shootProfile;
        private Transform _target;
        private CancellationTokenSource _shootTasksToken;

        private void Reset()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            _enemyAnimator.Attacked += OnAttack;
        }

        private void OnDisable()
        {
            _enemyAnimator.Attacked -= OnAttack;
            // StopShootTasks();
            //
            // void StopShootTasks() => 
            //     _shootTasksToken.Cancel();
        }

        private void OnAttack()
        {
            Shoot(_target.transform, _shootProfile.ProjectileInfo[0].Projectile);
        }

        public void SetShootProfile(ShootProfile shootProfile) =>
            _shootProfile = shootProfile;

        public void SetTarget(Transform target) =>
            _target = target;

        public void Shoot(Transform target, Projectile projectile)
        {
            var spawnPoint = Vector3.zero;

            switch (projectile)
            {
                case Bullet bullet:
                    spawnPoint = _shootPoints.BulletShootPoint.position; 
                    break;
                case Missile missile:
                    spawnPoint = _shootPoints.MissileShootPoint1.position;
                    break;
                case HomingMissile homingMissile:
                    spawnPoint = _shootPoints.MissileShootPoint2.position;
                    break;
            }
            
            var proj = Instantiate(projectile, spawnPoint, Quaternion.identity);
            proj.Launch(spawnPoint, target);
        }

        public void Shoot()
        {
            Shoot(_target, _shootProfile.ProjectileInfo[0].Projectile);
        }
    }
}