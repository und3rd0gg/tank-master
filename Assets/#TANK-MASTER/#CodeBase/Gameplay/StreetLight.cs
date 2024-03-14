using DG.Tweening;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.Logic;
using UnityEngine;
using VContainer;

namespace TankMaster.Gameplay
{
    public class StreetLight : MonoBehaviour
    {
        [SerializeField] private Light[] _lights;
        [SerializeField] private float _switchTime;
        [SerializeField] private float _intensity;

        private GlobalLight _globalLight;
        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }

        private void OnEnable() {
            // if (_gameFactory.MainLight != null)
            // {
            //     InitializeGlobalLight();
            // }
            // else
            // {
            //     _gameFactory.MainLightCreated += InitializeGlobalLight;
            // }
        }

        private void OnDisable()
        {
            _globalLight.DaytimeChanged -= GlobalLightOnDaytimeChanged;
        }

        private void InitializeGlobalLight()
        {
            _globalLight = _gameFactory.MainLight.GetComponent<GlobalLight>();
            _globalLight.DaytimeChanged += GlobalLightOnDaytimeChanged;
            InitializeStreetLight();
        }

        private void InitializeStreetLight()
        {
            GlobalLightOnDaytimeChanged(_globalLight.CurrentDaytime);
        }

        private void GlobalLightOnDaytimeChanged(DayTime currentDaytime)
        {
            if (currentDaytime == DayTime.Night)
            {
                SwitchLightsIntensity(_intensity);
            }
            else
            {
                foreach (var light in _lights)
                {
                    light.intensity = 0;
                }
            }
        }

        private void SwitchLightsIntensity(float intensity)
        {
            foreach (var light in _lights)
            {
                light.enabled = true;
                light.DOIntensity(intensity, _switchTime);
            }
        }
    }
}