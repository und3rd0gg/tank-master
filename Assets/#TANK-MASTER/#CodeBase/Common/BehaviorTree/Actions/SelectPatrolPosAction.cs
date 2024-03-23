using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public sealed class SelectPatrolPosAction : ActionBase
  {
    private readonly NPCProfile _npcProfile;
    private readonly EnemyNPCBase _npc;

    public SelectPatrolPosAction(NPCProfile npcProfile, EnemyNPCBase npc) {
      _npcProfile = npcProfile;
      _npc = npc;
    }
    
    protected override TaskStatus OnUpdate () {
      var randomRadius = Random.insideUnitCircle * _npcProfile.PatrolSettings.PatrolRadius;
      _npc.CurrentPatrolPos = _npc.InitialPos + new Vector3(randomRadius.x, 0, randomRadius.y);
      return TaskStatus.Success;
    }
  }
}