namespace TankMaster.Gameplay.Enemies.States
{
    public class Attack : IPayloadedState<IActor>
    {
        private readonly IActor _target;

        public Attack(IActor target)
        {
            _target = target;
        }

        public void Enter(IActor payload)
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}