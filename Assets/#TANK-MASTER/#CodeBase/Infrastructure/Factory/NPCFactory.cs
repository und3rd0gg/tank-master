using CleverCrow.Fluid.BTs.Trees;
using TankMaster.Gameplay.Actors.Enemies;
using UniExt.Dictionary;
using UnityEngine;
using VContainer;

namespace TankMaster.Infrastructure.Factory
{
  public class NPCFactory : MonoBehaviour
  {
    [SerializeField] private UniDict<EnemyType, Enemy> _enemyType;
    
    private IGameFactory _gameFactory;

    [Inject]
    internal void Construct(IGameFactory gameFactory) {
      _gameFactory = gameFactory;
    }

    public void CreateNPC(EnemyType enemyType, Vector3 creationPoint) {
      var enemy = _enemyType[enemyType];
      _gameFactory.Instantiate(enemy, creationPoint);
    }
  }
}