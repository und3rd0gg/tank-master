using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.NPC.Animators;
using TankMaster.Gameplay.Actors.NPC.Enemies;
using TankMaster.Gameplay.Actors.NPC.Enemies.Settings;
using TankMaster.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public class ChaseTargetAction : ActionBase
  {
    private readonly NavMeshAgent _agent;
    private readonly NPCProfile _npcProfile;
    private readonly Transform _transform;
    private readonly NPCAnimatorProvider _animator;
    private readonly EnemyNPCBase _npc;

    public ChaseTargetAction(EnemyNPCBase npc) {
      _npc = npc;
      _animator = npc.Animator;
      _npcProfile = npc.NpcProfile;
      _agent = npc.Agent;
      _transform = npc.transform;
    }
    protected override TaskStatus OnUpdate() {
      _agent.destination = _npc.DetectionBuffer[0].transform.position;
      _animator.SetMoveSpeed(_agent.velocity.magnitude / _agent.speed);

      if ((_transform.position - _agent.destination).sqrMagnitude < _npcProfile.ChaseSettings.SqrChaseStoppingDistance) {
        return TaskStatus.Success;
      }

      return TaskStatus.Continue;
    }
  }
}