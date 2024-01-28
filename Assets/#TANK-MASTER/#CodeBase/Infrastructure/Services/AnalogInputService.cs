using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Services
{
    public class AnalogInputService : IInputService
    {
        private PlayerInput _playerInput;

        public bool IsActive => true;
        public Vector2 MovementAxis => _playerInput.Player.Move.ReadValue<Vector2>();

        public void SetActive(bool state)
        {
            throw new System.NotImplementedException();
        }

        public void ShowVisuals() { }

        public void HideVisuals() { }

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