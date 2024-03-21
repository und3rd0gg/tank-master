using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Common.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay.Actors.Enemies
{
  public class Enemy : ActorBase
  {
    [SerializeField] private BehaviorTree _behaviorTree;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AttackBehaviorBase _attackBehavior;
    [SerializeField] private EnemyProfile _enemyProfile;
    
    private Collider[] _buffer = new Collider[1];
    private Vector3 _initialPos;
    private Vector3 _currentPatrolTarget;

    private bool _test;

    private void Awake() {
      _initialPos = transform.position;
      ConfigureBT();
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.A)) {
        _test = !_test;
      }

      if (Input.GetKeyDown(KeyCode.S)) {
        _behaviorTree.Reset();
      }
      
      _behaviorTree.Tick();
    }

    private void ConfigureBT() {
      _behaviorTree = new BehaviorTreeBuilder(gameObject)
        .Selector("Is player in sight?")
        .Parallel()
        .Inverter()
        .RepeatUntilSuccess()
        .Condition("Is player in vision zone?", IsPlayerInVisionZone)
        .End()
        .End()
        .RepeatForever()
        .Sequence()
        .Do("Select target position", () => {
          var randomRadius = Random.insideUnitCircle * _enemyProfile.PatrolSettings.PatrolRadius;
          _currentPatrolTarget = _initialPos + new Vector3(randomRadius.x, 0, randomRadius.y);
          return TaskStatus.Success;
        })
        .Do("Go to target", () => {
          _agent.SetDestination(_currentPatrolTarget);
          
          if (_agent.DestinationReached()) {
            return TaskStatus.Success;
          }
          
          return TaskStatus.Continue;
        })
        .WaitTime(_enemyProfile.PatrolSettings.WaitTime)
        .End()
        .End()
        .End()
        .Selector("Player left vision zone?")
        .Parallel()
        .RepeatUntilFailure()
        .Condition("Player left vision Zone", IsPlayerInVisionZone)
        .End()
        .Sequence()
        .Do("Move to target", () => {
          _agent.destination = _buffer[0].transform.position;

          if ((transform.position - _agent.destination).sqrMagnitude < _enemyProfile.ChaseSettings.SqrChaseStoppingDistance) {
            return TaskStatus.Success;
          }

          return TaskStatus.Continue;
        })
        .Do("Attack target", () => {
          _attackBehavior.Attack(null);
          return TaskStatus.Continue;
        })
        .End()
        .End()
        .Sequence()
        .Do("Stop", () => {
          _agent.SetDestination(Pivot.position);
          return TaskStatus.Success;
        })
        .WaitTime(_enemyProfile.ChaseSettings.TargetLostWaitTime)
        .End()
        .Build();
    }

    private bool IsPlayerInVisionZone() {
      var visionZoneSettings = _enemyProfile.VisionZoneSettings;
      var count = Physics.OverlapSphereNonAlloc(Pivot.position,
        visionZoneSettings.Radius, _buffer, visionZoneSettings.EnemyMask);

      if (count > 0) {
        return true;
      } else {
        return false;
      }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
      _enemyProfile.VisionZoneSettings.TryDrawGizmos(Pivot);
      _enemyProfile.ChaseSettings.TryDrawGizmos(transform);
    }
#endif
  }
}