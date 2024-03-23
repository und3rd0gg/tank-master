using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Common.BehaviorTree.Actions;
using TankMaster.Common.BehaviorTree.Conditions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Common.Extensions
{
  public static class BehaviorTreeBuilderExtensions
  {
    public static BehaviorTreeBuilder SelectPatrolPosAction(this BehaviorTreeBuilder builder, EnemyNPCBase npc,
      string name = "Select patrol position") {
      return builder.AddNode(new SelectPatrolPosAction(npc.NpcProfile, npc) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder MoveToPatrolPosAction(this BehaviorTreeBuilder builder, EnemyNPCBase npc,
      string name = "Move to patrol position") {
      return builder.AddNode(new MoveToPatrolPosAction(npc, npc.Agent) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder StopAction(this BehaviorTreeBuilder builder, NavMeshAgent agent, Transform pivot,
      string name = "Stop") {
      return builder.AddNode(new StopAction(agent, pivot) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder ChaseTargetAction(this BehaviorTreeBuilder builder, NavMeshAgent agent,
      EnemyNPCBase npc, NPCProfile npcProfile, string name = "ChaseTarget") {
      return builder.AddNode(new ChaseTargetAction(npc, npcProfile, agent) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder PlayerInVisionZoneCondition(this BehaviorTreeBuilder builder, EnemyNPCBase npc,
      NPCProfile npcProfile, string name = "ChaseTarget") {
      return builder.AddNode(new PlayerInVisionZoneCondition(npc, npcProfile) {
        Name = name,
      });
    }
  }
}