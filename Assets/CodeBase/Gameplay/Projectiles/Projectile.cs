using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public abstract void Launch(Vector3 startPosition, Vector3 target);
    }
}