using UnityEngine;

namespace TankMaster
{
    public class ShootPoints : MonoBehaviour
    {
        [field: SerializeField] public Transform BulletShootPoint { get; private set; }
        [field: SerializeField] public Transform MissileShootPoint1 { get; private set; }
        [field: SerializeField] public Transform MissileShootPoint2 { get; private set; }
    }
}
