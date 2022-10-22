using Dythervin.AutoAttach;
using TankMaster.Infrastructure;
using TankMaster.Infrastructure.Services;
using UnityEngine;

namespace TankMaster.Gameplay.MainPlayer
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Movement : MonoBehaviour
    {
        [SerializeField][Attach] private Rigidbody _rigidbody;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Update()
        {
            Debug.Log(_inputService.MovementAxis);
        }
    }
}