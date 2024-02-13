using TankMaster.Infrastructure.GameStates;
using UnityEngine;
using VContainer;

namespace TankMaster.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private IObjectResolver _objectResolver;

        [Inject]
        internal void Construct(IObjectResolver objectResolver) {
            _objectResolver = objectResolver;
        }

        private void Awake()
        {
            _game = new Game(_objectResolver);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}