using TankMaster.Common;
using TankMaster.Common.Physics;
using TankMaster.Gameplay.Actors.NPC.Enemies;
using TankMaster.Infrastructure.Services;
using TankMaster.Infrastructure.Services.SaveLoad;
using TankMaster.UI;
using UnityEngine;
using VContainer;

namespace TankMaster.Gameplay.Barriers
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
        private ISaveLoadService _saveLoadService;

        [Inject]
        internal void Construct(ISaveLoadService saveLoadService) {
            _saveLoadService = saveLoadService;
        }

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
                SaveProgress();
            }
        }

        private void SaveProgress()
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Saved!");
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