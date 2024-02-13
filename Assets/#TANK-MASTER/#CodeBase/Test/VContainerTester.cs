using TankMaster.Infrastructure.AssetManagement;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace TankMaster.Test
{
    public class VContainerTester : MonoBehaviour
    {
        [Inject]
        internal void Construct(IAssetProvider assetProvider, IGameFactory gameFactory, IAudioService audioService) {
            Debug.Log("тест");
            Debug.Assert(gameFactory != null);
            Debug.Assert(assetProvider != null);
        }       
    }
}