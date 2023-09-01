using Zenject;

namespace Infrastructure.GameStateMachine.States
{
    public class BootstrapGameState : IEnteringState
    {
        private const string LEVEL_NAME = "Start";

        private readonly IGameStateMachine stateMachine;
        public BootstrapGameState(IGameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        private void InitialiseServices()
        {
        }
        public void Enter()
        {
            InitialiseServices();
            stateMachine.Enter<LoadLevelState, string>(LEVEL_NAME);
        }

        public void Exit()
        {

        }

        public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapGameState>
        {

        }
    }
}