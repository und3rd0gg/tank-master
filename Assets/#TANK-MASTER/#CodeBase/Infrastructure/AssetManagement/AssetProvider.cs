using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TankMaster.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private GameObject _providerGo;

        private GameObject ProviderGO {
            get {
                if (_providerGo != null)
                    return _providerGo;
                
                _providerGo = new GameObject(nameof(AssetProvider));
                _providerGo.SetActive(false);
                return _providerGo;
            }
        }

        public async UniTask<GameObject> InstantiateAsync(string path, Vector3? creationPoint = null,
            Quaternion? rotation = null, Transform parent = null, bool enabled = true) {
            creationPoint ??= Vector3.zero;
            rotation ??= Quaternion.identity;

            AsyncOperationHandle<GameObject> createdObject;

            if (!enabled) {
                createdObject = Addressables
                    .InstantiateAsync(path, creationPoint.Value, rotation.Value,
                        parent: ProviderGO.transform);
                await createdObject.Task;
                createdObject.Result.SetActive(false);
                createdObject.Result.transform.parent = parent;
            } else {
                createdObject = Addressables
                    .InstantiateAsync(path, creationPoint.Value, rotation.Value, parent);
                await createdObject.Task;
            }

            return createdObject.Result;
        }

        public GameObject Instantiate(GameObject prefab, Vector3? creationPoint = null,
            Quaternion? rotation = null, Transform parent = null, bool dontDestroyOnLoad = false) {
            creationPoint ??= Vector3.zero;
            rotation ??= Quaternion.identity;

            GameObject createdObject = Object
                .Instantiate(prefab, creationPoint.Value, rotation.Value, parent);

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