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
    [SerializeField] private UniDict<EnemyType, Enemy> _enemyType;
    
    private IGameFactory _gameFactory;

    [Inject]
    internal void Construct(IGameFactory gameFactory) {
      _gameFactory = gameFactory;
    }

    public void CreateNPC(EnemyType enemyType, Vector3 creationPoint) {
      var npcProfile = _enemyType[enemyType];
      var npc =_gameFactory.Instantiate(npcProfile, creationPoint, enable: false);
      npc.SetBehaviorTree(SetupBehaviorTree(npc, npc.NpcProfile));
      npc.gameObject.SetActive(true);
    }

    private BehaviorTree SetupBehaviorTree(EnemyNPCBase npc, NPCProfile npcProfile) {
      return new BehaviorTreeBuilder(gameObject)
        .Selector("Is player in sight?")
        .Parallel()
        .Inverter()
        .RepeatUntilSuccess()
        .PlayerInVisionZoneCondition(npc, npcProfile)
        .End()
        .End()
        .RepeatForever()
        .Sequence("Patrol")
        .SelectPatrolPosAction(npc)
        .MoveToPatrolPosAction(npc)
        .WaitTime(npcProfile.PatrolSettings.WaitTime)
        .End()
        .End()
        .End()
        .Selector("Player left vision zone?")
        .Parallel()
        .RepeatUntilFailure()
        .PlayerInVisionZoneCondition(npc, npcProfile)
        .End()
        .Sequence()
        .ChaseTargetAction(npc.Agent, npc, npcProfile)
        .End()
        .End()
        .Sequence()
        .StopAction(npc.Agent, npc.Pivot)
        .WaitTime(npcProfile.ChaseSettings.TargetLostWaitTime)
        .End()
        .Build();
    }
  }
}