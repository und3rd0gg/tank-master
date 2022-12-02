using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.UI;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Barriers
{
    public class EnterBarrier : MonoBehaviour
    {
        [SerializeField] private Animator[] _barriersAnimators;
        [SerializeField] private TriggerObserver _triggerOpenObserver;
        [SerializeField] private TriggerObserver _triggerCloseObserver;
        [SerializeField] private GameObject _blocker;
        [SerializeField] private EnterTransitionLimitPresenter _limitPresenter;
        [SerializeField] private BoxCollider _boxCollider;

        private static readonly int IsOpened = Animator.StringToHash(nameof(IsOpened));

        private int _enemiesOnLevel;
        private int _currentEnemiesOnLevelCount;
        private int _killedEnemiesOnLevel;

        private void OnEnable()
        {
            _triggerOpenObserver.TriggerEnter += OnPlayerOpenZoneEnter;
            _triggerCloseObserver.TriggerEnter += OnPlayerOpenZoneExit;
        }

        private void OnDisable()
        {
            _triggerOpenObserver.TriggerEnter -= OnPlayerOpenZoneEnter;
            _triggerCloseObserver.TriggerEnter -= OnPlayerOpenZoneExit;
        }

        public void SetEnterLimitThreshold(Enemy[] enemies)
        {
            if (enemies == null)
            {
                _limitPresenter.UpdateText(0,0);
                return;
            }

            _enemiesOnLevel = enemies.Length;
            _currentEnemiesOnLevelCount = _enemiesOnLevel;
            _killedEnemiesOnLevel = 0;
            _limitPresenter.UpdateText(0, _enemiesOnLevel);
            
            foreach (var enemy in enemies)
            {
                enemy.Health.Died += OnEnemyDied;
            }
        }

        private void OnEnemyDied(Health enemyHealth)
        {
            enemyHealth.Died -= OnEnemyDied;
            _currentEnemiesOnLevelCount--;
            _killedEnemiesOnLevel++;
            _limitPresenter.UpdateText(_killedEnemiesOnLevel, _enemiesOnLevel);

            if (_killedEnemiesOnLevel == _enemiesOnLevel)
            {
                ActivateTrigger();
            }
        }

        private void ActivateTrigger()
        {
            _boxCollider.enabled = true;
        }

        private void OnPlayerOpenZoneEnter(Collider obj)
        {
            foreach (var barrier in _barriersAnimators)
            {
                barrier.SetBool(IsOpened, true);
                _blocker.SetActive(false);
            }
        }

        private void OnPlayerOpenZoneExit(Collider obj)
        {
            foreach (var barrier in _barriersAnimators)
            {
                barrier.SetBool(IsOpened, false);
                _blocker.SetActive(true);
            }
        }
    }
}