using TankMaster.Infrastructure.GameStates;
using VContainer;

namespace TankMaster.Infrastructure
{
    public class Game
    {
        private readonly IObjectResolver _objectResolver;
        public readonly GameStateMachine StateMachine;

        public Game(IObjectResolver objectResolver) {
            _objectResolver = objectResolver;
            StateMachine = new GameStateMachine(new SceneLoader(), _objectResolver);
        }
    }
}