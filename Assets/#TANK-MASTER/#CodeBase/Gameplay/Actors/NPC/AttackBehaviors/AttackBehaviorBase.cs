using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.AttackBehaviors
{
  public abstract class AttackBehaviorBase : MonoBehaviour
  {
    protected EnemyNPCBase NPC;

    public void Init(EnemyNPCBase npc) {
      NPC = npc;
    }

    public virtual void Attack(DamageableBase target) { }

    public virtual void Attack(Vector3 target) { }
    
    public virtual void Attack() { }
  }
}