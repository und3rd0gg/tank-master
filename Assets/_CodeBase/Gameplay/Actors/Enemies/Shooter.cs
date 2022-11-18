using TankMaster._CodeBase.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class Shooter : MonoBehaviour, IAttacker
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private Projectile _projectile;
        
        private Transform _target;

        private void Reset() => 
            enabled = false;

        private void OnEnable()
        {
            _enemyAnimator.SetAttack(true);
            _enemyAnimator.Attacked += OnAttack;
        }

        private void Update()
        {
            RotateToTarget(_target);
        }

        private void OnDisable()
        {
            _enemyAnimator.Attacked -= OnAttack;
            _enemyAnimator.SetAttack(false);
        }

        private void OnAttack()
        {
            Attack();
        }

        public void SetTarget(Transform target) =>
            _target = target;

        public bool IsInEffectiveDistance() =>
            Vector3.Distance(transform.position, _target.transform.position) <
            EffectiveDistance;

        private const float EffectiveDistance = 0;

        public void Attack()
        {
            var shootPoint = _shootPoint.position;
            var proj = Instantiate(_projectile, shootPoint, Quaternion.identity);
            proj.Launch(shootPoint, _target);
        }
        
        private void RotateToTarget(Transform target) => 
            transform.LookAt(target);
    }
}