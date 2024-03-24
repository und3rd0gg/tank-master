using System;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.NPC.Enemies;
using TankMaster.Gameplay.Actors.NPC.Enemies.Settings;
using UniExt.Dictionary;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
{
  [CreateAssetMenu(fileName = "NPC_DATABASE", menuName = "Infrastructure/NPC Database", order = 0)]
  public class NPCDB : ScriptableObject
  {
    public UniDict<NPCType, NPCInfo> NPCDict;
  }

  [Serializable]
  public class NPCInfo
  {
    public EnemyNPCBase NPC;
    public NPCProfile NPCProfile;
  }
}