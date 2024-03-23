using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Common.Extensions;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
  public class Enemy : EnemyNPCBase
  {
    [SerializeField] private AttackBehaviorBase _attackBehavior;
    
    private void Awake() {
      InitialPos = transform.position;
      //ConfigureBT();
    }

    private void Update() {
      BehaviorTree.Tick();
    }

    private void ConfigureBT() {
      BehaviorTree = new BehaviorTreeBuilder(gameObject)
        .Selector("Is player in sight?")
        .Parallel()
        .Inverter()
        .RepeatUntilSuccess()
        .PlayerInVisionZoneCondition(this, NpcProfile)
        .End()
        .End()
        .RepeatForever()
        .Sequence("Patrol")
        .SelectPatrolPosAction(this)
        .MoveToPatrolPosAction(this)
        .WaitTime(NpcProfile.PatrolSettings.WaitTime)
        .End()
        .End()
        .End()
        .Selector("Player left vision zone?")
        .Parallel()
        .RepeatUntilFailure()
        .PlayerInVisionZoneCondition(this, NpcProfile)
        .End()
        .Sequence()
        .ChaseTargetAction(Agent, this, NpcProfile)
        .End()
        .End()
        .Sequence()
        .StopAction(Agent, Pivot)
        .WaitTime(NpcProfile.ChaseSettings.TargetLostWaitTime)
        .End()
        .Build();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
      NpcProfile.VisionZoneSettings.TryDrawGizmos(Pivot);
      NpcProfile.ChaseSettings.TryDrawGizmos(transform);
    }
#endif
  }
}