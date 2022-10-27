using UnityEngine;

namespace TankMaster.Infrastructure.Services
{
    public interface IInputService : IService
    {
        public Vector2 MovementAxis { get; }
    }
}