using JetBrains.Annotations;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.Enemies
{
  public class KamikazeAnimatorProvider : NPCAnimatorProvider
  {
    private readonly int _selfDestroyHash = Animator.StringToHash("SelfDestroy");
        
    public void SetSelfDestroy() {
      Animator.SetTrigger(_selfDestroyHash);
    }
        
    [UsedImplicitly]
    public void OnSelfDestroy() {
      Debug.Log("VAR");
    }
  }
}