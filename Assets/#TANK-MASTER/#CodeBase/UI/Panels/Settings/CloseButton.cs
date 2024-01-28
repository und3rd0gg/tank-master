using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.UI.Panels
{
    public class CloseButton : Button
    {
        [SerializeField] private Panel _settingsPanel;

        protected override void OnClick()
        {
            _settingsPanel.Disable();
            AllServices.Container.Single<IInputService>().ShowVisuals();
        }
    }
}