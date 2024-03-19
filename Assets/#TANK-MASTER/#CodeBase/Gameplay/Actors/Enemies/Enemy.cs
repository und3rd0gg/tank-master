using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
    public class Enemy : ActorBase
    {
        [SerializeField] private BehaviorTree _behaviorTree;
        [SerializeField] private OverlapSettings _visionZoneSettings;

        private Collider[] _buffer = new Collider[1];

        private void Awake() {
            ConfigureBT();
        }

        private void Update() {
            _behaviorTree.Tick();
        }

        private void ConfigureBT() {
            _behaviorTree = new BehaviorTreeBuilder(gameObject)
                .Selector("")
                .Sequence()
                .Condition("Enemy In Vision", () => {
                    var count = Physics.OverlapSphereNonAlloc(_visionZoneSettings.OverlapPoint.position,
                        _visionZoneSettings.Radius,
                        _buffer, _visionZoneSettings.EnemyMask);
                    if (count > 0) {
                        return true;
                    } else {
                        return false;
                    }
                })
                .End()
                .Sequence()
                .Condition(() => false)
                .End()
                .End()
                .Build();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            _visionZoneSettings.TryDrawGizmos();
        }
#endif
    }
}