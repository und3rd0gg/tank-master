using UnityEngine;
using UnityEngine.Audio;

namespace TankMaster._CodeBase.UI.Panels
{
    public class SettingsPanel : Panel
    {
        [SerializeField] private AudioMixer _mixer;

        private const string VFX = nameof(VFX);
        private const string Music = nameof(Music);

        public void ChangeMusicVolume(float volume) => 
            _mixer.SetFloat(Music, GetVolume(volume));

        public void ChangeVFXVolume(float volume) => 
            _mixer.SetFloat(VFX, GetVolume(volume));

        private static float GetVolume(float volume) => 
            Mathf.Lerp(-80, 0, volume);
    }
}
