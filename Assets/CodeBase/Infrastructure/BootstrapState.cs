using System;
using TankMaster.Infrastructure.Services;

namespace TankMaster.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            RegisterServices();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private void RegisterServices()
        {
            RegisterInputService();
        }

        private static void RegisterInputService()
        {
            Game.InputService = new AnalogInputService();
        }
    }
}