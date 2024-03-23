using System;
using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Gameplay.Actors.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace TankMaster.Gameplay
{
  public abstract class DamageableBase : MonoBehaviour, IDamageable
  {
    [field: SerializeField] public Health Health { get; protected set; }
  }

  public abstract class ActorBase : DamageableBase, IActor
  {
    [field: SerializeField] public Transform Head { get; protected set; }
    [field: SerializeField] public Transform Chest { get; protected set; }
    [field: SerializeField] public Transform Pivot { get; protected set; }
  }

  public abstract class NPCBase : ActorBase, INPC
  {
    [SerializeField] protected BehaviorTree BehaviorTree;

    [field: SerializeField] public NPCProfile NpcProfile { get; protected set; }

    public void SetBehaviorTree(BehaviorTree behaviorTree) {
      BehaviorTree = behaviorTree;
    }
  }

  public abstract class EnemyNPCBase : NPCBase
  {
    [NonSerialized] public readonly Collider[] DetectionBuffer = new Collider[1];
    
    [NonSerialized] public Vector3 InitialPos;
    [NonSerialized] public Vector3 CurrentPatrolPos;

    [field: SerializeField] public NavMeshAgent Agent { get; protected set; }
  }

  public interface INPC
  {
    public NPCProfile NpcProfile { get; }
  }
}