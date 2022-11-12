using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class Shooter : MonoBehaviour, IAttacker
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private AttackProfile _attackProfile;
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
            Attack(_target);
        }

        public void SetShootProfile(AttackProfile attackProfile) =>
            _attackProfile = attackProfile;

        public void SetTarget(Transform target) =>
            _target = target;

        public bool IsInEffectiveDistance() =>
            Vector3.Distance(transform.position, _target.transform.position) <
            _attackProfile.EffectiveDistance;

        public void Attack(Transform target)
        {
            var shootPoint = _shootPoint.position;
            var proj = Instantiate(_attackProfile.ProjectileInfo[0].Projectile, shootPoint, Quaternion.identity);
            proj.Launch(shootPoint, target);
        }
    }
}