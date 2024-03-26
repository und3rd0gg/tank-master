using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.NPC.Animators;
using TankMaster.Gameplay.Actors.NPC.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public sealed class StopAction : ActionBase
  {
    private readonly NavMeshAgent _agent;
    private readonly Transform _pivot;
    private readonly NPCAnimatorProvider _animator;

    public StopAction(EnemyNPCBase npc) {
      _pivot = npc.Pivot;
      _agent = npc.Agent;
      _animator = npc.Animator;
    }
    
    protected override TaskStatus OnUpdate() {
      _agent.SetDestination(_pivot.position);
      _animator.SetMoveSpeed(0);
      return TaskStatus.Success;
    }
  }
}