namespace TankMaster._CodeBase.Infrastructure.Services
{
    public interface IAudioService : IService
    {
        public void ChangeVFXVolume(float volume);
        public void ChangeMusicVolume(float volume);
    }
}