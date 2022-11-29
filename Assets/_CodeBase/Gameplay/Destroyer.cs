using AYellowpaper;
using Unity.Mathematics;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private Coin _coin;
        [SerializeField] private int _coinsCount;
        [SerializeField] private InterfaceReference<IActor> _actor;
        [SerializeField] private ParticleSystem _destroyVFX;

        private static readonly float _coinCreationOffsetY = 0.5f;

        private void OnEnable()
        {
            _actor.Value.Health.Died += OnDied;
        }

        private void OnDisable()
        {
            _actor.Value.Health.Died -= OnDied;
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
            Instantiate(_destroyVFX, transform.position, quaternion.identity);
            InstantiateCoins();
        }

        private void InstantiateCoins()
        {
            for (var i = 0; i < _coinsCount; i++)
            {
                var creationPoint = transform.position;
                creationPoint.y += _coinCreationOffsetY;
                var coin = Instantiate(_coin, creationPoint, Quaternion.identity);
            }
        }

        private void OnDied() =>
            Destroy();
    }
}