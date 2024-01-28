using TankMaster._CodeBase.Data;

namespace TankMaster._CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }

    public interface IProgressSaver : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress playerProgress);
    }
}