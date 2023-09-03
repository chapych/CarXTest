using Infrastructure.Services.GameFactory;
using Infrastructure.Services.SceneLoaderService;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.PointsForStaticData;
using Infrastructure.Services.StaticDataService;
using Infrastructure.Services.StaticDataService.PointsForStaticData;
using Infrastructure.Services.StaticDataService.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IPayloadedEnteringState<string>
    {
        private readonly IGameStateMachine stateMachine;
        private readonly ISceneLoader sceneLoader;
        private readonly IStaticDataService staticData;
        private readonly IGameFactory factory;

        public LoadLevelState(IGameStateMachine stateMachine,
            ISceneLoader sceneLoader,
            IStaticDataService staticData,
            IGameFactory factory)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.factory = factory;
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
            string name = SceneManager.GetActiveScene().name;
            LevelStaticData forLevel = staticData.ForLevel(name);

            foreach (TowerPoint towerPoint in forLevel.TowerPoints)
            {
                factory.CreateTower(towerPoint.TowerBaseType, towerPoint.WeaponType, towerPoint.Position,
                    towerPoint.Range, towerPoint.ShootInterval);
            }

            foreach (SpawnerPoint spawner in forLevel.SpawnerPoints)
            {
                factory.CreateSpawner(spawner.Position, spawner.Interval, spawner.MoveTargetPosition, spawner.Speed, spawner.MaxHP);
            }
        }

        public void Exit()
        {
            factory.CleanUp();
        }
        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState> { }
    }
}