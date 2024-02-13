using UnityEngine;

namespace TankMaster.Test
{
    public class Camera : MonoBehaviour, ICamera
    {
        public float FOV { get; }
    }

    public interface ICamera
    {
        public float FOV { get; }
        public Transform transform { get; }
    }
}