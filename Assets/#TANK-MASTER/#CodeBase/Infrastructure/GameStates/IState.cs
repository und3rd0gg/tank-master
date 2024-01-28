namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}