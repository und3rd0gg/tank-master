using CleverCrow.Fluid.BTs.Tasks;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.NPC.Enemies.Settings;
using UnityEngine;

namespace TankMaster.Common.BehaviorTree.Conditions
{
  public class PlayerInVisionZoneCondition : ConditionBase
  {
    private readonly EnemyNPCBase _npc;
    private readonly NPCProfile _npcProfile;
    private readonly Transform _pivot;

    public PlayerInVisionZoneCondition(EnemyNPCBase npc) {
      _npc = npc;
      _npcProfile = npc.NpcProfile;
      _pivot = npc.Pivot;
    }
    
    protected override bool OnUpdate() {
      var visionZoneSettings = _npcProfile.VisionZoneSettings;
      var count = UnityEngine.Physics.OverlapSphereNonAlloc(_pivot.position,
        visionZoneSettings.Radius, _npc.DetectionBuffer, visionZoneSettings.EnemyMask);

      if (count > 0) {
        return true;
      } else {
        return false;
      }
    }
  }
}