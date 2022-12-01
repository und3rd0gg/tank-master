using TankMaster._CodeBase.UI.Panels;
using TankMaster._CodeBase.UI.Store;
using UnityEngine;

namespace TankMaster._CodeBase.UI
{
    public class Interface : MonoBehaviour
    {
        [field: SerializeField] public Panel Store { get; private set; }
    }
}