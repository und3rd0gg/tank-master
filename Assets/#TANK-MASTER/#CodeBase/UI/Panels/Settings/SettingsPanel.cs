using TankMaster.Infrastructure.Services;
using VContainer;

namespace TankMaster.UI.Panels.Settings
{
    public class SettingsPanel : Panel
    {
        private IAudioService _audioService;

        [Inject]
        internal void Construct(IAudioService audioService) {
            _audioService = audioService;
        }
        
        public void ChangeMusicVolume(float volume) =>
            _audioService.ChangeMusicVolume(volume);

        public void ChangeVFXVolume(float volume) => 
            _audioService.ChangeVFXVolume(volume);
    }
}
