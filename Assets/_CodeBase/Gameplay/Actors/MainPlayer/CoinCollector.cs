using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField][Attach] private Player _player;
        
        [SerializeField] private PhysicsDetector _physicsDetector;

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
            Destroy(coin.gameObject);
        }

        private void PullCoins()
        {
            var coins = _physicsDetector.DetectedObjects;

            foreach (var coin in coins)
            {
                if (coin != null)
                {
                    var coinPosition = coin.transform.position;
                    coin.transform.position = Vector3.Lerp(coinPosition, transform.position, Time.deltaTime * 3f);
                }
            }
        }
    }
}