using CleverCrow.Fluid.BTs.Tasks;
using Drawing;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.NPC.Enemies.Settings;
using UnityEngine;

namespace TankMaster.Common.BehaviorTree.Conditions
{
  public class EffectiveDistanceReachedCondition : ConditionBase
  {
    private readonly NPCProfile _profile;
    private readonly EnemyNPCBase _npc;
    private readonly Transform _transform;

    public EffectiveDistanceReachedCondition(EnemyNPCBase npc) {
      _npc = npc;
      _profile = npc.NpcProfile;
      _transform = npc.Pivot;
    }
    
    protected override bool OnUpdate() {
#if UNITY_EDITOR
      
      using (Draw.WithColor(Color.red)) {
        Draw.xz.Circle(_npc.transform.position, _profile.AttackSettings.StoppingDistance);
      }
      
#endif
      
      if ((_transform.position - _npc.DetectionBuffer[0].transform.position).sqrMagnitude <
          _profile.AttackSettings.StoppingDistance) {
        return true;
      }

      return false;
    }
  }
}