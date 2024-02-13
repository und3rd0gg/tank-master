using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private IInputService _inputService;

        [Inject]
        internal void Construct(IInputService inputService) {
            _inputService = inputService;
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            float motorDelta = _inputService.MovementAxis.y;
            float steeringDelta = _inputService.MovementAxis.x;
        }
    }
}