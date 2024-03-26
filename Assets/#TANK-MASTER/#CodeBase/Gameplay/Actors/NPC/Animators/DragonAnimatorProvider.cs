using System;
using JetBrains.Annotations;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Animators
{
  public sealed class DragonAnimatorProvider : NPCAnimatorProvider
  {
    private readonly int _attackHash = Animator.StringToHash("Attack");
        
    private Action _onAttack;

    public void SetAttack(Action onAttack) {
      _onAttack = onAttack;
      Animator.SetTrigger(_attackHash);
    }
        
    [UsedImplicitly]
    public void OnAttack() {
      _onAttack();
    }
  }
}