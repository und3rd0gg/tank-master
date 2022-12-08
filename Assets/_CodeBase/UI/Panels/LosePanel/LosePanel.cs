using TankMaster._CodeBase.Infrastructure.Services;

namespace TankMaster._CodeBase.UI.Panels
{
    public class LosePanel : Panel
    {
        public override void Enable()
        {
            base.Enable();
            AllServices.Container.Single<IInputService>().HideVisuals();
        }
    }
}