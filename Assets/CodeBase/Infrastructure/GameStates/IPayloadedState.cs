namespace TankMaster.Infrastructure.GameStates
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}