using System;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Data;
using TankMaster.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class Player : ActorBase, IProgressSaver
    {
        [field: SerializeField] public Transform CameraFollowTarget { get; private set; }
        [field: SerializeField] public BulletShooter BulletShooter { get; private set; }
        [field: SerializeField] public BulletShooter MissileShooter { get; private set; }
        
        [SerializeField] private Money _money;
        
        public Money Money => _money;

        public BehaviorTree _behaviorTree;

        private void Awake() {
            _behaviorTree = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                .Condition("Custom Condition", () => {
                    return true;
                })
                .Do("Custom Action", () => {
                    return TaskStatus.Success;
                })
                .End()
                .Build();
        }

        public void LoadProgress(PlayerProgress playerProgress) => 
            _money.LoadProgress(playerProgress);

        public void UpdateProgress(PlayerProgress playerProgress) => 
            _money.UpdateProgress(playerProgress);
    }
}