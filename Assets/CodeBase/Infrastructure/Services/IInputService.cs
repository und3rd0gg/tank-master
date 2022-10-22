using UnityEngine;

namespace TankMaster.Infrastructure.Services
{
    public interface IInputService
    {
        public Vector2 MovementAxis { get; }
    }
}