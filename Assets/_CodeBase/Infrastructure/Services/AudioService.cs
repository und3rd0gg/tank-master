using UnityEngine;
using UnityEngine.Audio;

namespace TankMaster._CodeBase.Infrastructure.Services
{
    public class AudioService : MonoService, IAudioService
    {
        [SerializeField] private AudioMixer _audioMixer;
        
        private const string Master = nameof(Master);
        private const string VFX = nameof(VFX);
        private const string Music = nameof(Music);
        private const float LowSoundValue = -80;
        private const float HighSoundValue = 0;

        protected override void InitializeService() { }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                UnmuteSound();
            else
                MuteSound();
        }

        public void ChangeMasterVolume(float volume) => 
            _audioMixer.SetFloat(Master, GetVolume(volume));

        public void ChangeMusicVolume(float volume) => 
            _audioMixer.SetFloat(Music, GetVolume(volume));

        public void MuteSound() => 
            ChangeMasterVolume(0);

        public void UnmuteSound() => 
            ChangeMasterVolume(1);

        public void ChangeVFXVolume(float volume) => 
            _audioMixer.SetFloat(VFX, GetVolume(volume));

        private float GetVolume(float volume) => 
            UnityExtensions.Remap.DoRemap(0, 1, LowSoundValue, HighSoundValue, volume);
    }
}