using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.Gameplay.Tutorial
{
    public class MovementTutorialEnd : MonoBehaviour
    {
        private IInputService _inputService;

        [Inject]
        internal void Construct(IInputService inputService) {
            _inputService = inputService;
        }
        
        public void DisableJoystick() => 
            _inputService.HideVisuals();

        public void EnableJoystick() => 
            _inputService.ShowVisuals();
    }
}