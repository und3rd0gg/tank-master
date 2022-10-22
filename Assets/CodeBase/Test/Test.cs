using UnityEngine;

namespace TankMaster.Test
{
    public class Test : MonoBehaviour
    {
        private PlayerInput _playerInput;
        
        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.Move.performed += context => LogMove();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void LogMove()
        {
            var inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
            Debug.Log(inputVector);
        }
    }
}
