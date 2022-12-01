﻿using System;
using BuildingBlocks.DataTypes;
using DG.Tweening;
using Dythervin.AutoAttach;
using TankMaster._CodeBase.Infrastructure;
using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    public class GlobalLight : MonoBehaviour
    {
        [SerializeField][Attach] private Light _light;

        [SerializeField] private InspectableDictionary<DayTime, DaytimeSettings> DaytimeSettingsMap;
        [SerializeField] private float _switchTime;

        public DayTime CurrentDaytime { get; private set; } = DayTime.Day;

        public event Action<DayTime> DaytimeChanged;

        public void SetNextDayTime()
        {
            SwitchDayTime(CurrentDaytime.Next());
        }

        public void SwitchDayTime(DayTime dayTime)
        {
            var daytimeSettings = DaytimeSettingsMap[dayTime];
            CurrentDaytime = dayTime;
            _light.DOIntensity(daytimeSettings.Intensity, _switchTime);
            _light.transform.DORotate(daytimeSettings.Rotation.eulerAngles, _switchTime);
            _light.DOColor(daytimeSettings.Color, _switchTime);
            DaytimeChanged?.Invoke(CurrentDaytime);
        }
    }

    public enum DayTime
    {
        Day = 0,
        Evening = 1,
        Night = 2
    }
}