using UnityEngine;

namespace TankMaster.Infrastructure.Services
{
    public class AnalogInputService : IInputService
    {
        private PlayerInput _playerInput;

        public Vector2 MovementAxis => _playerInput.Player.Move.ReadValue<Vector2>();

        public AnalogInputService()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        ~AnalogInputService()
        {
            _playerInput.Disable();
        }
    }
}