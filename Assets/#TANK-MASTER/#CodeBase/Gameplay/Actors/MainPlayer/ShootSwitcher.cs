using UnityEngine;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class ShootSwitcher : MonoBehaviour
    {
        // [SerializeField] private PhysicsDetector _detector;
        // [SerializeField] private TurretRotator _turretRotator;
        //
        // private IAttacker[] _attackers;
        //
        // private void Awake()
        // {
        //     InitializeShooters();
        // }
        //
        // private void Update()
        // {
        //     SwitchComponents();
        // }
        //
        // private void SwitchComponents()
        // {
        //     var targetObject = _detector.GetClosestObject();
        //
        //     if (targetObject != null)
        //     {
        //         _turretRotator.EnableRotation(targetObject);
        //         EnableAttackers(targetObject);
        //     }
        //     else
        //     {
        //         _turretRotator.DisableRotation();
        //         DisableAttackers();
        //     }
        // }
        //
        // private void EnableAttackers(Transform targetObject)
        // {
        //     foreach (var attacker in _attackers)
        //     {
        //         attacker.SetTarget(targetObject);
        //         attacker.enabled = true;
        //     }
        // }
        //
        // private void DisableAttackers()
        // {
        //     foreach (var attacker in _attackers)
        //     {
        //         attacker.enabled = false;
        //     }
        // }
        //
        // private void InitializeShooters()
        // {
        //     _attackers = GetComponents<IAttacker>();
        // }
    }
}