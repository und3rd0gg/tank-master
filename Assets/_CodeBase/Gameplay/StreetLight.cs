using System.Linq;
using DG.Tweening;
using TankMaster._CodeBase.Infrastructure.Factory;
using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.Logic;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    public class StreetLight : MonoBehaviour
    {
        [SerializeField] private Light[] _lights;
        [SerializeField] private float _switchTime;
        [SerializeField] private float _intensity;

        private GlobalLight _globalLight;

        private void OnEnable()
        {
            var gameFactiory = AllServices.Container.Single<IGameFactory>();

            if (gameFactiory.MainLight != null)
            {
                InitializeGlobalLight();
            }
            else
            {
                gameFactiory.MainLightCreated += InitializeGlobalLight;
            }
        }

        private void OnDisable()
        {
            _globalLight.DaytimeChanged -= GlobalLightOnDaytimeChanged;
        }

        private void InitializeGlobalLight()
        {
            _globalLight = AllServices.Container.Single<IGameFactory>().MainLight.GetComponent<GlobalLight>();
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