using UnityEngine;

namespace TankMaster.Infrastructure.Services
{
    public class TouchInputService : IInputService
    {
        private Joystick _joystick;

        public Vector2 MovementAxis => _joystick.Direction;
    }
}