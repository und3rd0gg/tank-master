using DavidJalbert.TinyCarControllerAdvance;
using Dythervin.AutoAttach;
using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.MainPlayer
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
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