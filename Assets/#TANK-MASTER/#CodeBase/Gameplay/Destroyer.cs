using AYellowpaper;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using TankMaster.UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace TankMaster.Gameplay
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private Coin _coin;
        [SerializeField] private int _coinsCount;
        [SerializeField] private InterfaceReference<IActor> _actor;
        [SerializeField] private ParticleSystem _destroyVFX;
        [SerializeField] private UnityEvent _destroyCallback;

        private static readonly float _coinCreationOffsetY = 0.5f;
        private IGameFactory _gameFactory;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
        }

        private void OnEnable()
        {
            _actor.Value.Health.Died += OnDied;
        }

        private void OnDisable()
        {
            _actor.Value.Health.Died -= OnDied;
        }

        public void InstantiateCoins()
        {
            for (var i = 0; i < _coinsCount; i++)
            {
                var creationPoint = transform.position;
                creationPoint.y += _coinCreationOffsetY;
                var coin = Instantiate(_coin, creationPoint, Quaternion.identity);
            }
        }

        public void ShowLoseScreen() => 
            _gameFactory.Interface.GetComponent<Interface>().LosePanel.Enable();

        public void DeleteObject() => 
            Destroy(gameObject);

        public virtual void Destroy()
        {
            Instantiate(_destroyVFX, transform.position, quaternion.identity);
            _destroyCallback?.Invoke();
        }

        private void OnDied(Health health) =>
            Destroy();
    }
}