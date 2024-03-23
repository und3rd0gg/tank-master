using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public class ChaseTargetAction : ActionBase
  {
    private readonly NavMeshAgent _agent;
    private readonly NPCProfile _npcProfile;
    private readonly Transform _transform;
    private readonly EnemyNPCBase _npc;

    public ChaseTargetAction(EnemyNPCBase npc, NPCProfile npcProfile, NavMeshAgent agent) {
      _npc = npc;
      _npcProfile = npcProfile;
      _agent = agent;
      _transform = npc.transform;
    }
    protected override TaskStatus OnUpdate() {
      _agent.destination = _npc.DetectionBuffer[0].transform.position;

      if ((_transform.position - _agent.destination).sqrMagnitude < _npcProfile.ChaseSettings.SqrChaseStoppingDistance) {
        return TaskStatus.Success;
      }

      return TaskStatus.Continue;
    }
  }
}