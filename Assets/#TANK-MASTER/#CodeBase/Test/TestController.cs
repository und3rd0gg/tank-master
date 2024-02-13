using UnityEngine;

namespace TankMaster.Test
{
    public class TestController : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("InterstitialAd");
                //InterstitialAd.Show();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("VideoAd");
                //VideoAd.Show();
            }
        }
    }
}