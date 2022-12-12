using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Services
{
    public abstract class MonoService : MonoBehaviour, IService
    {
        public static MonoService instance = null;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            InitializeService();
        }

        protected abstract void InitializeService();
    }
}