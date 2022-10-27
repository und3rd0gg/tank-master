using TankMaster.Infrastructure.Services;
using UnityEngine;

namespace TankMaster.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public GameObject Instantiate(string path);
        public GameObject Instantiate(string path, Vector3 creationPoint, Quaternion startRotation);
    }
}