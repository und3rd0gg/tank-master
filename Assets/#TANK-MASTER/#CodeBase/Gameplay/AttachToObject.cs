using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    public class AttachToObject : MonoBehaviour
    {
        [SerializeField] private Transform _object;

        private void Update()
        {
            transform.position = _object.position;
        }
    }
}