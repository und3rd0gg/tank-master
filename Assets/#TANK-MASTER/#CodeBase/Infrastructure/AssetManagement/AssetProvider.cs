using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TankMaster.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public async UniTask<GameObject> InstantiateAsync(string path, Vector3? creationPoint = null,
            Quaternion? rotation = null, Transform parent = null, bool dontDestroyOnLoad = false) {
            creationPoint ??= Vector3.zero;
            rotation ??= Quaternion.identity;

            AsyncOperationHandle<GameObject> createdObject = Addressables
                .InstantiateAsync(path, (Vector3)creationPoint, (Quaternion)rotation, parent);
            await createdObject.Task;

            if (dontDestroyOnLoad && createdObject.Result != null) {
                Object.DontDestroyOnLoad(createdObject.Result);
            }

            return createdObject.Result;
        }

        public GameObject Instantiate(GameObject prefab, Vector3? creationPoint = null,
            Quaternion? rotation = null, Transform parent = null, bool dontDestroyOnLoad = false) {
            creationPoint ??= Vector3.zero;
            rotation ??= Quaternion.identity;
            
            GameObject createdObject = Object
                .Instantiate(prefab, (Vector3)creationPoint, (Quaternion)rotation, parent);
            
            if (dontDestroyOnLoad) {
                Object.DontDestroyOnLoad(createdObject);
            }

            return createdObject;
        }

        public async UniTask<IList<GameObject>> LoadAll(string path) {
            IList<GameObject> assets = await Addressables.LoadAssetsAsync<GameObject>(path, null);
            return assets;
        }

        public async UniTask<GameObject> Load(string path) {
            return await Addressables.LoadAssetAsync<GameObject>(path).Task;
        }
    }
}