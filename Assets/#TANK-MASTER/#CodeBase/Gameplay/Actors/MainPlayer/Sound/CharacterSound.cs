using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer.Sound
{
    public class CharacterSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _engineSound;
        [SerializeField] private MovementAlongSurface _engine;

        [Header("Sound Values")] [SerializeField]
        private float _engineLowPitch;

        [SerializeField] private float _engineHighPitch;
        [SerializeField] private float _engineHighPitchSecondary;
        [SerializeField] private float _pitchIncreaseSpeed;

        private void OnEnable()
        {
            Debug.Assert(_engineSound != null);
            Debug.Assert(_engine != null);
            
            _engine.EngineStarted += OnEngineStarted;
            _engine.EngineStopped += OnEngineStopped;
            _engineSound.Play();
        }

        private void OnDisable()
        {
            _engine.EngineStarted -= OnEngineStarted;
            _engine.EngineStopped -= OnEngineStopped;
        }

        public void EngineAccelerate()
        {
            if (Mathf.Approximately(_engineSound.pitch, _engineHighPitch))
                _engineSound.pitch = _engineHighPitchSecondary;
            else
                _engineSound.pitch =
                    Mathf.MoveTowards(_engineSound.pitch, _engineHighPitch, Time.deltaTime * _pitchIncreaseSpeed);
        }

        public void EngineDeccelerate()
        {
            _engineSound.pitch =
                Mathf.MoveTowards(_engineSound.pitch, _engineLowPitch, Time.deltaTime * _pitchIncreaseSpeed);
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