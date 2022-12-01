using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(GameObject prefab, Vector3 creationPoint) => 
            GameObject.Instantiate(prefab, creationPoint, Quaternion.identity);

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

        public GameObject Instantiate(string path, Vector3 creationPoint)
        {
            var prefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(prefab, creationPoint, prefab.transform.rotation);
        }

        public GameObject[] LoadAll(string path) => 
            Resources.LoadAll<GameObject>(path);

        public GameObject Load(string path) => 
            Resources.Load<GameObject>(path);
    }
}