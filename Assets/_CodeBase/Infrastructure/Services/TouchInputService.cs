using TankMaster._CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Services
{
    public class TouchInputService : IInputService
    {
        private readonly UltimateJoystick _joystick;

        public TouchInputService()
        {
            _joystick = AllServices.Container.Single<IGameFactory>().CreateJoystick();
        }

        public bool IsActive() => 
            _joystick.GetJoystickState();

        public Vector2 MovementAxis => 
            new(_joystick.GetHorizontalAxis(), _joystick.GetVerticalAxis());

        public void ShowVisuals() => 
            _joystick.EnableJoystick();

        public void HideVisuals() => 
            _joystick.DisableJoystick();
    }
}