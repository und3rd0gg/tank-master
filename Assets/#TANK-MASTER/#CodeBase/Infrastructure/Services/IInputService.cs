using UnityEngine;

namespace TankMaster.Infrastructure.Services
{
    public interface IInputService : IService
    {
        public bool IsActive { get; }
        
        public Vector2 MovementAxis { get; }
        public void ShowVisuals();
        public void HideVisuals();
    }
}