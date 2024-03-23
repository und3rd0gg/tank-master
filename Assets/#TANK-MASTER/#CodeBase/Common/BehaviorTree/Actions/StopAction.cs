using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public sealed class StopAction : ActionBase
  {
    private readonly NavMeshAgent _agent;
    private readonly Transform _pivot;

    public StopAction(NavMeshAgent agent, Transform pivot) {
      _pivot = pivot;
      _agent = agent;
    }
    
    protected override TaskStatus OnUpdate() {
      _agent.SetDestination(_pivot.position);
      return TaskStatus.Success;
    }
  }
}