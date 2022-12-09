using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Dythervin.AutoAttach;
using TankMaster._CodeBase.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors
{
    public class AggroSwitcher : MonoBehaviour
    {
        [SerializeField][Attach]  private Follower _follower;
        
        [SerializeField] private TriggerObserver _detectionZoneObserver;
        [SerializeField] private float _chaseCooldown;
        
        private CancellationTokenSource _chaseCooldownCancellationTokenSource = new();

        private void OnEnable()
        {
            _detectionZoneObserver.TriggerEnter += DetectionZoneEnter;
            _detectionZoneObserver.TriggerExit += DetectionZoneExit;
        }

        private void OnDisable()
        {
            _detectionZoneObserver.TriggerEnter -= DetectionZoneEnter;
        }

        public void StartAggro()
        {
            DetectionZoneExit(null);
            _follower.enabled = true;
        }

        public void StopAggro() => 
            _follower.enabled = false;

        private void DetectionZoneEnter(Collider target)
        {
            _chaseCooldownCancellationTokenSource.Cancel();
            _follower.SetTarget(target.gameObject.transform);
            _follower.enabled = true;
        }

        private void DetectionZoneExit(Collider target)
        {
            ResetToken();
            ChaseCooldownAsync(_chaseCooldownCancellationTokenSource.Token);
        }
        
        private async UniTask ChaseCooldownAsync(CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_chaseCooldown),
                cancellationToken: cancellationToken);
            StopAggro();
        }

        private void ResetToken() => 
            _chaseCooldownCancellationTokenSource = new CancellationTokenSource();
    }
}