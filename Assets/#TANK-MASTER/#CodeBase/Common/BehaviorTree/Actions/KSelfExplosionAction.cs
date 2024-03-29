using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors.NPC.Animators;
using UnityEngine;

namespace TankMaster.Common.BehaviorTree.Actions
{
  public class KSelfExplosionAction : ActionBase
  {
    private readonly KamikazeAnimatorProvider _animator;
    private readonly EnemyNPCBase _npc;

    public KSelfExplosionAction(EnemyNPCBase npc) {
      _npc = npc;
      _animator = (KamikazeAnimatorProvider)npc.Animator;
    }

    protected override TaskStatus OnUpdate() {
      _animator.SetSelfDestroy(OnSelfDestroy);
      return TaskStatus.Continue;
    }

    private void OnSelfDestroy() {
      var targets =
        UnityEngine.Physics.OverlapSphere(_npc.transform.position, _npc.NpcProfile.AttackSettings.StoppingDistance);

      for (var i = 0; i < targets.Length; i++) {
        var damageables = targets[i].GetComponentsInChildren<DamageableBase>();

        for (var j = 0; j < damageables.Length; j++) {
          damageables[j].Health.ApplyDamage(_npc.NpcProfile.AttackSettings.BaseDamage);
        }
      }

      var explosion = Object.Instantiate(_npc.NpcProfile.DeathSettings.DeathParticle, _npc.transform.position,
        Quaternion.identity);
      explosion.Play();
      _npc.Health.ApplyDamage(_npc.Health.MaxValue);
    }
  }
}