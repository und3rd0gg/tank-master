using TankMaster.Gameplay.Actors.Enemies;
using TankMaster.Infrastructure;
using TankMaster.Infrastructure.Factory;
using UnityEngine;
using VContainer;

namespace TankMaster.Logic
{
    public class ChangeDateTimeTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private int _daytimeSwitchChance;

        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += TriggerObserverOnTriggerEnter;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerObserverOnTriggerEnter;
        }

        private void TriggerObserverOnTriggerEnter(Collider obj)
        {
            if (UnityExtensions.RandomChance(_daytimeSwitchChance))
                SwitchDaytime();
            
            enabled = false;
        }

        private void SwitchDaytime()
        {
            var daytimeSwitcher =
                _gameFactory.MainLight.GetComponent<GlobalLight>();
            
            daytimeSwitcher.SetNextDayTime();
        }
    }
}