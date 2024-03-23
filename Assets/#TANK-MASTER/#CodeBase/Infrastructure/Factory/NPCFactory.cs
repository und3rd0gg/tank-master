using System;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Common.Extensions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.Enemies;
using UniExt.Dictionary;
using UnityEngine;
using VContainer;

namespace TankMaster.Infrastructure.Factory
{
  public class NPCFactory : MonoBehaviour
  {
    [SerializeField] private UniDict<NPCType, Enemy> _enemyType;

    private IGameFactory _gameFactory;

    [Inject]
    internal void Construct(IGameFactory gameFactory) {
      _gameFactory = gameFactory;
    }

    public void CreateNPC(NPCType npcType, Vector3 creationPoint) {
      var npcProfile = _enemyType[npcType];
      var npc = _gameFactory.Instantiate(npcProfile, creationPoint, enable: false);
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
  }
}