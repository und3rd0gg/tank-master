using TankMaster.UI.HUD;
using TankMaster.UI.Panels;
using UnityEngine;

namespace TankMaster.UI
{
    public class Interface : MonoBehaviour
    {
        private void OnEnable() {
            Debug.Log("enabled");
        }private void OnDisable() {
            Debug.Log("disabled ");
        }

        [field: SerializeField] public Panel Store { get; private set; }
        [field: SerializeField] public Panel LosePanel { get; private set; }
        [field: SerializeField] public BalancePresenter BalancePresenter { get; private set; }
    }
}