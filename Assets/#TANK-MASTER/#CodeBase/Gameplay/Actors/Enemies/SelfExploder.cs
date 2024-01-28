using System.Collections.Generic;

using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class SelfExploder : MonoBehaviour, IAttacker
    {
        [SerializeField]private Destroyer _destroyer;
        
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private float _impactRadius;
        [SerializeField] private uint _damage;

        private void OnEnable()
        {
            _enemyAnimator.SetAttack(true);
            _enemyAnimator.Attacked += OnAttack;
        }

        private void OnDisable()
        {
            _enemyAnimator.Attacked -= OnAttack;
        }

        public void SetTarget(Transform target) { }

        private void OnAttack()
        {
            foreach (var damageable in GetDamageables())
            {
                damageable.ApplyDamage(_damage);
            }
            
            _destroyer.Destroy();
        }
        
        private List<IDamageable> GetDamageables()
        {
            var impactedObjects = Physics.OverlapSphere(transform.position, _impactRadius);
            var damageables = new List<IDamageable>(impactedObjects.Length);

            foreach (var impactedObject in impactedObjects)
            {
                if (impactedObject.TryGetComponent(out IDamageable damageable))
                {
                    damageables.Add(damageable);
                }
            }

            return damageables;
        }
    }
}