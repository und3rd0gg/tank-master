namespace TankMaster.Gameplay.Enemies.States
{
    public interface IExittableState
    {
        public void Exit();
    }

    public interface ITickableState : IExittableState
    {
        public void Tick();
    }

    public interface IDefaultState : ITickableState
    {
        public void Enter();
    }
    
    public interface IPayloadedState<TPayload> : ITickableState
    {
        public void Enter(TPayload payload);
    }
}