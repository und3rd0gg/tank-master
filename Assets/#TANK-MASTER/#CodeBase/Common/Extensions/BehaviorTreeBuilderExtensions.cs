using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Common.BehaviorTree.Actions;
using TankMaster.Common.BehaviorTree.Conditions;
using TankMaster.Gameplay;
using TankMaster.Infrastructure.Factory;
using UnityEngine;

namespace TankMaster.Common.Extensions
{
  public static class BehaviorTreeBuilderExtensions
  {
#region Actions

    public static BehaviorTreeBuilder SelectPatrolPosAction(this BehaviorTreeBuilder builder, EnemyNPCBase npc,
      string name = "Select patrol position") {
      return builder.AddNode(new SelectPatrolPosAction(npc.NpcProfile, npc) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder MoveToPatrolPosAction(this BehaviorTreeBuilder builder, EnemyNPCBase npc,
      string name = "Move to patrol position") {
      return builder.AddNode(new MoveToPatrolPosAction(npc) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder StopAction(this BehaviorTreeBuilder builder, EnemyNPCBase npc,
      string name = "Stop") {
      return builder.AddNode(new StopAction(npc) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder ChaseTargetAction(this BehaviorTreeBuilder builder,
      EnemyNPCBase npc, string name = "Chase target") {
      return builder.AddNode(new ChaseTargetAction(npc) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder SelfExplosionAction(this BehaviorTreeBuilder builder,
      EnemyNPCBase npc, string name = "Self explosion") {
      return builder.AddNode(new KSelfExplosionAction(npc) {
        Name = name,
      });
    }

#endregion

#region Conditions

    public static BehaviorTreeBuilder PlayerInVisionZoneCondition(this BehaviorTreeBuilder builder, EnemyNPCBase npc,
      string name = "Player in vision zone") {
      return builder.AddNode(new PlayerInVisionZoneCondition(npc) {
        Name = name,
      });
    }

    public static BehaviorTreeBuilder EffectiveDistanceReachedCondition(this BehaviorTreeBuilder builder,
      EnemyNPCBase npc, string name = "Effective distance reached") {
      return builder.AddNode(new EffectiveDistanceReachedCondition(npc) {
        Name = name,
      });
    }

#endregion
  }
}