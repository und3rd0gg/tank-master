using TankMaster.Infrastructure.AssetManagement;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(Vector3 creationPoint)
        {
            return _assetProvider.Instantiate(AssetPaths.MainPlayer, creationPoint, Quaternion.Euler(0, 90, 0));
        }
    }
}