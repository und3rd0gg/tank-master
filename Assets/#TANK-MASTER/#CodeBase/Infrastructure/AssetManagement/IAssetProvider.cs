using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TankMaster.Infrastructure.Services;
using UnityEngine;

namespace TankMaster.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public UniTask<IList<GameObject>> LoadAll(string path);
        UniTask<GameObject> Load(string path);

        UniTask<GameObject> InstantiateAsync(string path, Vector3? creationPoint = null,
            Quaternion? rotation = null, Transform parent = null, bool dontDestroyOnLoad = false);

        GameObject Instantiate(GameObject prefab, Vector3? creationPoint = null,
            Quaternion? rotation = null, Transform parent = null, bool dontDestroyOnLoad = false);
    }
}