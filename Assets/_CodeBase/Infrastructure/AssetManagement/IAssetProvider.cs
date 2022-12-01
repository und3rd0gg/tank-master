using TankMaster._CodeBase.Infrastructure.Services;
using UnityEngine;

namespace TankMaster._CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public GameObject Instantiate(string path, Vector3 creationPoint, Quaternion startRotation);
        public GameObject Instantiate(string path, Vector3 creationPoint);
        public GameObject[] LoadAll(string path);
        GameObject Load(string path);
        GameObject Instantiate(GameObject prefab, Vector3 creationPoint);
    }
}