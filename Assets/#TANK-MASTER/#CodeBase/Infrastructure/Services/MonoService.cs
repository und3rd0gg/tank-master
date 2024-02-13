using UnityEngine;

namespace TankMaster.Infrastructure.Services
{
    public abstract class MonoService : MonoBehaviour, IService
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            InitializeService();
        }

        protected abstract void InitializeService();
    }
}