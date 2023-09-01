using Infrastructure.GameBootstrapper;
using Infrastructure.GameStateMachine;
using Infrastructure.Services.SceneLoader;
using Infrastructure.Services.SceneLoaderService;
using Infrastructure.Services.StaticData;
using Zenject;

namespace Bindings
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGame();
            BindSceneLoader();
            BindGameStateMachine();
            BindStaticDataService();
        }

        private void BindStaticDataService()
        {
            Container.Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle();
        }

        private void BindGame()
        {
            Container.Bind<Game>()
                .AsSingle();
        }
        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .AsSingle();
        }
        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();
        }
    }
}