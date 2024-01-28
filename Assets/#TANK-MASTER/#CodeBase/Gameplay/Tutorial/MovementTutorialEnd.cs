using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Tutorial
{
    public class MovementTutorialEnd : MonoBehaviour
    {
        public void DisableJoystick() => 
            AllServices.Container.Single<IInputService>().HideVisuals();

        public void EnableJoystick() => 
            AllServices.Container.Single<IInputService>().ShowVisuals();
    }
}