using TankMaster.Gameplay.Projectiles;
using UnityEngine;

namespace TankMaster.Gameplay
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Projectile _projectile;

        public void Shoot(Vector3 target)
        {
            var projectile = Instantiate(_projectile, _shootPoint.position, Quaternion.identity);
            projectile.Launch(_shootPoint.position, target);
        }
    }
}