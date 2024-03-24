using System;
using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Common.Extensions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.NPC.Enemies;
using TankMaster.Gameplay.Actors.NPC.Enemies.Settings;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
{
  public class NPCFactory
  {
    private readonly IGameFactory _gameFactory;
    private readonly NPCDB _npcDB;

    public NPCFactory(IGameFactory gameFactory, NPCDB npcdb) {
      _npcDB = npcdb;
      _gameFactory = gameFactory;
    }

    public void CreateNPC(NPCType npcType, Vector3 creationPoint) {
      var npcInfo = _npcDB.NPCDict[npcType];
      var npc = _gameFactory.Instantiate(npcInfo.NPC, creationPoint, enable: false);
      npc.SetProfile(npcInfo.NPCProfile);
      npc.SetBehaviorTree(GetBehaviorTree(npc, npc.NpcProfile));
      npc.gameObject.SetActive(true);
    }

    private BehaviorTree GetBehaviorTree(EnemyNPCBase npc, NPCProfile npcProfile) {
      var bt = new BehaviorTreeBuilder(npc.gameObject)
        .Selector("Is player in sight?")
        .Splice(GetPatrolBehavior(npc))
        .Selector("Player left vision zone?")
        .Parallel()
        .RepeatUntilFailure()
        .PlayerInVisionZoneCondition(npc)
        .End()
        .Splice(GetAttackBehavior(npc))
        .End()
        .Sequence()
        .StopAction(npc)
        .WaitTime(npcProfile.ChaseSettings.TargetLostWaitTime)
        .End()
        .End()
        .Build();

      return bt;
    }

    private BehaviorTree GetPatrolBehavior(EnemyNPCBase npc) {
      return new BehaviorTreeBuilder(npc.gameObject)
        .Parallel()
        .Inverter()
        .RepeatUntilSuccess()
        .PlayerInVisionZoneCondition(npc)
        .End()
        .End()
        .RepeatForever()
        .Sequence("Patrol")
        .SelectPatrolPosAction(npc)
        .MoveToPatrolPosAction(npc)
        .WaitTime(npc.NpcProfile.PatrolSettings.WaitTime)
        .End()
        .End()
        .End()
        .Build();
    }

    private BehaviorTree GetAttackBehavior(EnemyNPCBase npc) {
      var bt = new BehaviorTreeBuilder(npc.gameObject);

      switch (npc.NpcType) {
        case NPCType.Kamikaze:
          GetKamikazeAttackBeh(npc, bt);
          break;
        case NPCType.Soldier:
          break;
        case NPCType.Dragon:
          break;
        case NPCType.Random:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return bt.Build();
    }
    
    private void GetDragonAttackBeh(EnemyNPCBase npc, BehaviorTreeBuilder bt) {
      bt.Selector("Effective distance reached?")
        .Parallel()
        .Inverter()
        .RepeatUntilSuccess()
        .EffectiveDistanceReachedCondition(npc)
        .End()
        .End()
        .ChaseTargetAction(npc)
        .End()
        .Sequence()
        .StopAction(npc)
        .SelfExplosionAction(npc)
        .End()
        .End();
    }

    private void GetKamikazeAttackBeh(EnemyNPCBase npc, BehaviorTreeBuilder bt) {
      bt.Selector("Effective distance reached?")
        .Parallel()
        .Inverter()
        .RepeatUntilSuccess()
        .EffectiveDistanceReachedCondition(npc)
        .End()
        .End()
        .ChaseTargetAction(npc)
        .End()
        .Sequence()
        .StopAction(npc)
        .SelfExplosionAction(npc)
        .End()
        .End();
    }
  }
}