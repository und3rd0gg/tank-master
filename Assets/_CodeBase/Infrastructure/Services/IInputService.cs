using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Services
{
    public interface IInputService : IService
    {
        public bool IsActive();
        public Vector2 MovementAxis { get; }
        public void ShowVisuals();
        public void HideVisuals();
    }
}