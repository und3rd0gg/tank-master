using UnityEngine.AI;

namespace TankMaster.Common.Extensions
{
  public static class NavMeshAgentExtensions
  {
    public static bool DestinationReached(this NavMeshAgent agent) {
      if (!agent.pathPending) {
        if (agent.remainingDistance <= agent.stoppingDistance) {
          if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
            return true;
          }
        }
      }

      return false;
    }
  }
}