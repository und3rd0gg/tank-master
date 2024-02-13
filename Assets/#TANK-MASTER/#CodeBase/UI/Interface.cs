using TankMaster.UI.Panels;
using UnityEngine;

namespace TankMaster.UI
{
    public class Interface : MonoBehaviour
    {
        [field: SerializeField] public Panel Store { get; private set; }
        [field: SerializeField] public Panel LosePanel { get; private set; }
        [field: SerializeField] public BalancePresenter BalancePresenter { get; private set; }
    }
}