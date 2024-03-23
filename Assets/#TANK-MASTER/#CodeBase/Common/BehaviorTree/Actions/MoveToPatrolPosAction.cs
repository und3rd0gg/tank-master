using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Common.Extensions;
using TankMaster.Gameplay;
using UnityEngine.AI;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public sealed class MoveToPatrolPosAction : ActionBase
  {
    private readonly NavMeshAgent _agent;
    private readonly EnemyNPCBase _npc;

    public MoveToPatrolPosAction(EnemyNPCBase npc, NavMeshAgent agent) {
      _npc = npc;
      _agent = agent;
    }

    protected override TaskStatus OnUpdate() {
      _agent.SetDestination(_npc.CurrentPatrolPos);
          
      if (_agent.DestinationReached()) {
        return TaskStatus.Success;
      }
          
      return TaskStatus.Continue;
    }
  }
}