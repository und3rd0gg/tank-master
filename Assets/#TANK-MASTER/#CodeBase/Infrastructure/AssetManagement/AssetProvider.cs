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

        public GameObject Instantiate(string path, Vector3 creationPoint, bool dontDestroyOnLoad = false)
        {
            var prefab = Resources.Load<GameObject>(path);
            var obj = GameObject.Instantiate(prefab, creationPoint, prefab.transform.rotation);
            
            if(dontDestroyOnLoad)
                GameObject.DontDestroyOnLoad(obj);

            return obj;
        }

        public GameObject[] LoadAll(string path) => 
            Resources.LoadAll<GameObject>(path);

        public GameObject Load(string path) => 
            Resources.Load<GameObject>(path);
    }
}