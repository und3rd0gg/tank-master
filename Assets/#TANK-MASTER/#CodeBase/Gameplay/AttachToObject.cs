using UnityEngine;

namespace TankMaster.Gameplay
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