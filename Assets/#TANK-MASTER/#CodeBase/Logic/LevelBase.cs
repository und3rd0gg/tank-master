using Dreamteck.Splines;
using UnityEngine;

namespace TankMaster.Logic
{
    public abstract class LevelBase : MonoBehaviour
    {
        [field: SerializeField] public SplineComputer Path { get; protected set; }
    }
}