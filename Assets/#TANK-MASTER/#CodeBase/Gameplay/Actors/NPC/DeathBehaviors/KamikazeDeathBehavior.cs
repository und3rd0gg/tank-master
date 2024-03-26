using MoreMountains.Feedbacks;
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.DeathBehaviors
{
  public class KamikazeDeathBehavior : DeathBehaviorBase
  {
    [SerializeField] private MMF_Player _mmfPlayer;

    public override void OnDeath(Health health) {
      _mmfPlayer.PlayFeedbacks();
      Destroy(gameObject, _mmfPlayer.TotalDuration);
    }
  }
}