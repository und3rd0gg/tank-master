
using UnityEngine;

namespace TankMaster.Gameplay.Actors.NPC.Enemies
{
    public class ShootingEnemy : Enemy
    {
        [SerializeField]private Shooter _shooter;

        private void Awake()
        {
            //InitializeShooterComponent();
        }
    }
}