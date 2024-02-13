using TankMaster.Infrastructure.Services;
using TankMaster.UI.Panels;
using UnityEngine;
using VContainer;

namespace TankMaster.UI
{
    public class OpenSettingsWindowButton : Button
    {
        [SerializeField] private Panel _settingWindow;
        private IInputService _inputService;

        [Inject]
        internal void Construct(IInputService inputService) {
            _inputService = inputService;
        }
        
        protected override void OnClick()
        {
            _inputService.HideVisuals();
            _settingWindow.Enable();
        }
    }
}