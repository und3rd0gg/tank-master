namespace TankMaster._CodeBase.Infrastructure.GameStates
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}