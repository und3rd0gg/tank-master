using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Common.Extensions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine.AI;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public sealed class MoveToPatrolPosAction : ActionBase
  {
    private readonly NavMeshAgent _agent;
    private readonly EnemyNPCBase _npc;
    private readonly NPCAnimatorProvider _animator;

    public MoveToPatrolPosAction(EnemyNPCBase npc) {
      _npc = npc;
      _agent = npc.Agent;
      _animator = _npc.Animator;
    }

    protected override TaskStatus OnUpdate() {
      _agent.SetDestination(_npc.CurrentPatrolPos);
      _animator.SetMoveSpeed(_agent.velocity.magnitude / _agent.speed);
          
      if (_agent.DestinationReached()) {
        return TaskStatus.Success;
      }
          
      return TaskStatus.Continue;
    }
  }
}