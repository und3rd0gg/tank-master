
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        [SerializeField] private AudioSource _coinSource;
        //[SerializeField] private PhysicsDetector _physicsDetector;

        private int CoinLayer;

        private Money Money => _player.Money;

        private void Awake()
        {
            CoinLayer = LayerMask.NameToLayer("Coin");
        }

        private void FixedUpdate()
        {
            PullCoins();
        }

        private void OnTriggerEnter(Collider coin)
        {
            CheckCoinTriggerEnter(coin);
        }

        private void CheckCoinTriggerEnter(Collider coin)
        {
            if (coin.gameObject.layer != CoinLayer) return;

            Money.Add(1);
            _coinSource.Play();
            Destroy(coin.gameObject);
        }

        private void PullCoins()
        {
            // var coins = _physicsDetector.DetectedObjects;
            //
            // foreach (var coin in coins)
            // {
            //     if (coin != null)
            //     {
            //         var coinPosition = coin.transform.position;
            //         coin.transform.position = Vector3.Lerp(coinPosition, transform.position, Time.deltaTime * 5.5f);
            //     }
            // }
        }
    }
}