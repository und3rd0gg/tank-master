using TankMaster._CodeBase.UI.Panels;
using UnityEngine;

namespace TankMaster._CodeBase.UI
{
    public class Interface : MonoBehaviour
    {
        [field: SerializeField] public Panel Store { get; private set; }
        [field: SerializeField] public Panel LosePanel { get; private set; }
        [field: SerializeField] public BalancePresenter BalancePresenter { get; private set; }
    }
}