using UnityEngine;

namespace TankMaster.UI
{
    public class RotateToObject : MonoBehaviour
    {
        [SerializeField] private Transform _object;

        private void LateUpdate()
        {
            transform.LookAt(_object);
        }
    }
}
