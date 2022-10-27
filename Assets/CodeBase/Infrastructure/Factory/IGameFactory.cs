using TankMaster.Infrastructure.Services;
using UnityEngine;

namespace TankMaster.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 creationPoint);
    }
}