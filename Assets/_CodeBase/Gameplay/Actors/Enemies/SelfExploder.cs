using Dythervin.AutoAttach;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors
{
    public class SelfExploder : MonoBehaviour, IAttacker
    {
        [SerializeField][Attach] private Destroyer _destroyer;
        
        [SerializeField] private EnemyAnimator _enemyAnimator;

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

        private void OnAttack() => 
            _destroyer.Destroy();
    }
}