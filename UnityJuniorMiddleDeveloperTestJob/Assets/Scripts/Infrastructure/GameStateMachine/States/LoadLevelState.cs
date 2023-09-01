using Infrastructure.Services.SceneLoaderService;
using UnityEngine;
using Zenject;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IPayloadedEnteringState<string>
    {
        private readonly IGameStateMachine stateMachine;
        private readonly ISceneLoader sceneLoader;

        public LoadLevelState(IGameStateMachine stateMachine,
            ISceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter(string scene)
        {
            sceneLoader.Load(scene, Init);
        }

        private void Init()
        {
            InitGameWorld();
        }

        private void InitGameWorld()
        {
            
        }

        public void Exit() { }
        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState> { }
    }
}