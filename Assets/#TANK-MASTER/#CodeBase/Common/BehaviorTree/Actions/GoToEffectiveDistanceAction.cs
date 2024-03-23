using System;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Gameplay;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public class GoToEffectiveDistanceAction : ActionBase
  {
    private readonly EnemyNPCBase _npc;

    public GoToEffectiveDistanceAction(EnemyNPCBase npc) {
      _npc = npc;
    }
    
    protected override TaskStatus OnUpdate() {
      throw new NotImplementedException();
    }
  }
}