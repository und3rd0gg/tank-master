using System.Threading;
using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private ShootProfile _shootProfile;
        private Transform _target;

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
        }

        private void OnAttack()
        {
            Shoot(_target.transform, _shootProfile.ProjectileInfo[0].Projectile);
        }

        public void SetShootProfile(ShootProfile shootProfile) =>
            _shootProfile = shootProfile;

        public void SetTarget(Transform target) =>
            _target = target;

        public bool IsInEffectiveDistance() =>
            Vector3.Distance(transform.position, _target.transform.position) <
            _shootProfile.EffectiveDistance;

        public void Shoot(Transform target, Projectile projectile)
        {
            var shootPoint = _shootPoint.position;
            var proj = Instantiate(projectile, shootPoint, Quaternion.identity);
            proj.Launch(shootPoint, target);
        }
    }
}