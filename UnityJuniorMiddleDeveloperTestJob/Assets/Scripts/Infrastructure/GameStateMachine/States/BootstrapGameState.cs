using System.Threading.Tasks;
using Infrastructure.Services.AssetProviderService;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Infrastructure.GameStateMachine.States
{
    public class BootstrapGameState : IEnteringState
    {
        private const string LEVEL_NAME = "ForTest";

        private readonly IGameStateMachine stateMachine;
        private readonly IStaticDataService staticDataService;
        private readonly IAssetProvider assetProvider;

        public BootstrapGameState(IGameStateMachine stateMachine, IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            this.stateMachine = stateMachine;
            this.staticDataService = staticDataService;
            this.assetProvider = assetProvider;
        }

        private async Task InitialiseServices()
        {
            assetProvider.Initialise();
            await staticDataService.Load();

        }
        public async Task Enter()
        {
            await InitialiseServices();
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