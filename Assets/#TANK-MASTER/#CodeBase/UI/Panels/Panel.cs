using UnityEngine;

namespace TankMaster._CodeBase.UI.Panels
{
    public class Panel : MonoBehaviour
    {
        public bool IsStoppingTimeOnEnable = true;
        
        public virtual void Enable()
        {
            if (IsStoppingTimeOnEnable)
                Time.timeScale = 0;
            
            gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}