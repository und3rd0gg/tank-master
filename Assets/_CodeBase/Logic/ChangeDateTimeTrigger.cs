using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using TankMaster._CodeBase.Infrastructure;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    public class ChangeDateTimeTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private int _daytimeSwitchChance;

        private IGameFactory _gameFactory;

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

        private static void SwitchDaytime()
        {
            var daytimeSwitcher =
                AllServices.Container.Single<IGameFactory>().MainLight.GetComponent<GlobalLight>();
            
            daytimeSwitcher.SetNextDayTime();
        }
    }
}