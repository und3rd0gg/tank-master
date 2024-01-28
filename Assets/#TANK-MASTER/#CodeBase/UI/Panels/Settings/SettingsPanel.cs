using TankMaster._CodeBase.Infrastructure.Services;

namespace TankMaster._CodeBase.UI.Panels
{
    public class SettingsPanel : Panel
    {
        public void ChangeMusicVolume(float volume) =>
            AllServices.Container.Single<IAudioService>().ChangeMusicVolume(volume);

        public void ChangeVFXVolume(float volume) => 
            AllServices.Container.Single<IAudioService>().ChangeVFXVolume(volume);
    }
}
