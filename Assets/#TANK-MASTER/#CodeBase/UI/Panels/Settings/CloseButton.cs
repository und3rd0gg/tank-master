using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.Panels.Settings
{
    public class CloseButton : Button
    {
        [SerializeField] private Panel _settingsPanel;
        private IInputService _inputService;

        [Inject]
        internal void Construct(IInputService inputService) {
            _inputService = inputService;
        }

        protected override void OnClick()
        {
            _settingsPanel.Disable();
            _inputService.ShowVisuals();
        }
    }
}