using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies
{
  [RequireComponent(typeof(Animator))]
  public class AnimatorProviderBase : MonoBehaviour
  {
    [SerializeField] protected Animator Animator;
    
    public Animator GetAnimator() => 
      Animator;
  }
}