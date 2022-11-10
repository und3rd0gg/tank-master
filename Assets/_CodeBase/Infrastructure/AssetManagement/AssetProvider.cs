using UnityEngine;

namespace TankMaster.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 creationPoint, Quaternion startRotation)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, creationPoint, startRotation);
        }
    }
}