using System;
using Cysharp.Threading.Tasks;
using TankMaster.Infrastructure.Factory;
using UnityEngine;

namespace TankMaster.Infrastructure.Services
{
    public class TouchInputService : IInputService
    {
        private UltimateJoystick _joystick;
        private IGameFactory _gameFactory;

        public TouchInputService(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
            CreateJoystick(HideVisuals).Forget();
        }

        private async UniTaskVoid CreateJoystick(Action onComplete = null) {
            _joystick = await _gameFactory.CreateJoystick();
            onComplete?.Invoke();
        }

        public bool IsActive =>
            _joystick.GetJoystickState();

        public Vector2 MovementAxis => 
            new(_joystick.GetHorizontalAxis(), _joystick.GetVerticalAxis());

        public void ShowVisuals() => 
            _joystick.EnableJoystick();

        public void HideVisuals() => 
            _joystick.DisableJoystick();
    }
}