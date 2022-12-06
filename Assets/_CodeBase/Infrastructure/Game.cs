using TankMaster._CodeBase.Infrastructure.GameStates;

namespace TankMaster._CodeBase.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine(new SceneLoader());
        }
    }
}