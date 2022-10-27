using Dythervin.AutoAttach;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using DavidJalbert.TinyCarControllerAdvance;

namespace TankMaster.Gameplay.MainPlayer
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField][Attach] private TCCAPlayer _controllableObject;

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
            _controllableObject.setMotor(motorDelta);
            _controllableObject.setSteering(steeringDelta);
        }
    }
}