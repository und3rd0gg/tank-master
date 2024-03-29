﻿using System;
using CleverCrow.Fluid.BTs.Trees;
using Sirenix.OdinInspector;
using TankMaster.Gameplay.Actors.NPC.Animators;
using TankMaster.Gameplay.Actors.NPC.AttackBehaviors;
using TankMaster.Gameplay.Actors.NPC.DeathBehaviors;
using TankMaster.Gameplay.Actors.NPC.Enemies;
using TankMaster.Gameplay.Actors.NPC.Enemies.Settings;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace TankMaster.Gameplay
{
  public abstract class DamageableBase : MonoBehaviour, IDamageable
  {
    [field: SerializeField] public Health Health { get; protected set; }

    public event Action Destroyed = delegate { };

    [Button]
    public void Kill() {
      Health.ApplyDamage(Health.MaxValue);
    }

    private void OnDestroy() {
      Destroyed();
    }
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

    [field: SerializeField] public NPCType NpcType { get; protected set; }

    public NPCProfile NpcProfile { get; protected set; }

    protected virtual void Update() {
      BehaviorTree.Tick();
    }

    public void SetBehaviorTree(BehaviorTree behaviorTree) {
      BehaviorTree = behaviorTree;
    }

    public void SetProfile(NPCProfile profile) {
      NpcProfile = profile;
    }
  }

  public abstract class EnemyNPCBase : NPCBase
  {
    [FormerlySerializedAs("_deathAction")] [SerializeField] private DeathBehaviorBase _deathBehavior;

    [NonSerialized] public readonly Collider[] DetectionBuffer = new Collider[1];

    [NonSerialized] public Vector3 InitialPos;
    [NonSerialized] public Vector3 CurrentPatrolPos;

    [field: SerializeField] public NavMeshAgent Agent { get; protected set; }
    [field: SerializeField] public NPCAnimatorProvider Animator { get; protected set; }
    [field: SerializeField] public AttackBehaviorBase AttackBehavior { get; protected set; }

    protected virtual void Awake() {
      InitialPos = transform.position;
      AttackBehavior.Init(this);
    }

    protected virtual void OnEnable() {
      Health.Died += _deathBehavior.OnDeath;
    }

    protected void OnDisable() {
      Health.Died -= _deathBehavior.OnDeath;
    }
  }
}