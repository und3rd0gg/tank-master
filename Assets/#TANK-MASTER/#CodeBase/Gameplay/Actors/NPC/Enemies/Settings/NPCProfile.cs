using System;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies.Settings
{
  [CreateAssetMenu(fileName = nameof(NPCProfile), menuName = "Gameplay/" + nameof(NPCProfile))]
  public class NPCProfile : ScriptableObject
  {
    [field: SerializeField] public OverlapSettings VisionZoneSettings { get; private set; }
    [field: SerializeField] public ChaseSettings ChaseSettings { get; private set; }
    [field: SerializeField] public PatrolSettings PatrolSettings { get; private set; }
    [field: SerializeField] public AttackSettings AttackSettings { get; private set; }
    [field: SerializeField] public DeathSettings DeathSettings { get; private set; }
  }
  
  [Serializable]
  public class DeathSettings
  {
    [field: SerializeField] public ParticleSystem DeathParticle { get; private set; }
  }
  
  [Serializable]
  public class AttackSettings
  {
    [field: SerializeField] public float StoppingDistance { get; private set; }
    [field: SerializeField] public int BaseDamage { get; private set; }
  }
}