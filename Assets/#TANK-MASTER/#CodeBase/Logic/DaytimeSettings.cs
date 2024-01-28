using UnityEngine;

namespace TankMaster._CodeBase.Logic
{
    [CreateAssetMenu(fileName = "New Daytime Settings", menuName = "Gameplay/Daytime Settings")]
    public class DaytimeSettings : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }
        [field: SerializeField] public float Intensity { get; private set; }
    }
}