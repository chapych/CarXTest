namespace Infrastructure.GameStateMachine
{
    public interface IEnteringState : IExitableState
    {
        void Enter();
    }
}