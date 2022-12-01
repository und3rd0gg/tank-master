using UnityEngine;

namespace TankMaster._CodeBase.UI.Panels
{
    public class Panel : MonoBehaviour
    {
        public bool IsStoppingTime = true;
        
        public virtual void Enable()
        {
            if (IsStoppingTime)
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