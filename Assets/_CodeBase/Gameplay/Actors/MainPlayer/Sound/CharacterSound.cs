using System;
using TMPro;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer.Sound
{
    public class CharacterSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _engineSound;
        [SerializeField] private MovementAlongSurface _engine;
        
        private void OnEnable()
        {
            _engine.EngineStarted += OnEngineStarted;
            _engine.EngineStopped += OnEngineStopped;
            _engineSound.Play();
        }

        private void OnDisable()
        {
            _engine.EngineStarted -= OnEngineStarted;
            _engine.EngineStopped -= OnEngineStopped;
        }

        private void OnEngineStarted()
        {
            _engineSound.Play();
        }
        
        private void OnEngineStopped()
        {
            _engineSound.Stop();
        }
    }
}