using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.Services
{
    public interface IInputService : IService
    {
        public Vector2 MovementAxis { get; }
    }
}