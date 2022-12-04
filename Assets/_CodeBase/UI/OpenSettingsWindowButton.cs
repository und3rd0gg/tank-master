using TankMaster._CodeBase.Infrastructure.Services;
using TankMaster._CodeBase.UI.Panels;
using UnityEngine;

namespace TankMaster._CodeBase.UI
{
    public class OpenSettingsWindowButton : Button
    {
        [SerializeField] private Panel _settingWindow;

        protected override void OnClick()
        {
            AllServices.Container.Single<IInputService>().HideVisuals();
            _settingWindow.Enable();
        }
    }
}