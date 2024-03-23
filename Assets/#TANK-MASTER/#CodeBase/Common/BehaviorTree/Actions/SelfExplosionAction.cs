using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.Enemies;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public class SelfExplosionAction : ActionBase
  {
    private readonly EnemyNPCBase _npc;
    private readonly AttackBehaviorBase _attackBehavior;
    private readonly KamikazeAnimatorProvider _animator;

    public SelfExplosionAction(EnemyNPCBase npc) {
      _npc = npc;
      _animator = (KamikazeAnimatorProvider)npc.Animator;
      _attackBehavior = npc.AttackBehavior;
    }

    protected override TaskStatus OnUpdate() {
      _animator.SetSelfDestroy();
      return TaskStatus.Continue;
    }
  }
}