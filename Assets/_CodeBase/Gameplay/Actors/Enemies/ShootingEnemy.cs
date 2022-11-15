using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class ShootingEnemy : Enemy
    {
        [SerializeField][Attach] private Shooter _shooter;

        private void Awake()
        {
            //InitializeShooterComponent();
        }
    }
}