using System;
using JetBrains.Annotations;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies
{
  public class KamikazeAnimatorProvider : NPCAnimatorProvider
  {
    private readonly int _selfDestroyHash = Animator.StringToHash("SelfDestroy");
    
    private Action _onSelfDestroy;
        
    public void SetSelfDestroy(Action onSelfDestroy) {
      _onSelfDestroy = onSelfDestroy;
      Animator.SetTrigger(_selfDestroyHash);
    }
        
    [UsedImplicitly]
    public void OnSelfDestroy() {
      _onSelfDestroy();
    }
  }
}