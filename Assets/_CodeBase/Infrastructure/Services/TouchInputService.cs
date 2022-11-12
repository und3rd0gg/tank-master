using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Services
{
    public class TouchInputService : IInputService
    {
        private Joystick _joystick;

        public Vector2 MovementAxis => _joystick.Direction;
    }
}