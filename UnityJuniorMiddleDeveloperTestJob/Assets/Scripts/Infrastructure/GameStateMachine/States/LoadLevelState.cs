using System.Collections.Generic;
using System.Threading.Tasks;
using BaseInterfaces.Gameplay;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.SceneLoaderService;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.PointsForStaticData;
using Infrastructure.Services.StaticDataService;
using Infrastructure.Services.StaticDataService.PointsForStaticData;
using Infrastructure.Services.StaticDataService.StaticData;
using Logic;
using Logic.Tower;
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
        private readonly ICoroutineRunnerProvider coroutineRunnerProvider;
        private Dictionary<Task<GameObject>, StopWatch> creatingMonsterSpawners;
        private Dictionary<Task<GameObject>, StopWatch> creatingTowers;

        public LoadLevelState(IGameStateMachine stateMachine,
            ISceneLoader sceneLoader,
            IStaticDataService staticData,
            IGameFactory factory,
            ICoroutineRunnerProvider coroutineRunnerProvider)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
            this.staticData = staticData;
            this.factory = factory;
            this.coroutineRunnerProvider = coroutineRunnerProvider;
        }

        public void Enter(string scene)
        {
            sceneLoader.Load(scene, Init);
        }

        private void Init()
        {
            Debug.Log("d");
            InitGameWorld();
        }

        private void InitGameWorld()
        {
            ICoroutineRunner coroutineRunner = coroutineRunnerProvider.GetCoroutineRunner();

            string name = SceneManager.GetActiveScene().name;
            LevelStaticData forLevel = staticData.ForLevel(name);

            creatingTowers = new Dictionary<Task<GameObject>, StopWatch>(forLevel.TowerPoints.Count);
            creatingMonsterSpawners = new Dictionary<Task<GameObject>, StopWatch>(forLevel.SpawnerPoints.Count);
            Debug.Log("one");
            foreach (TowerPoint towerPoint in forLevel.TowerPoints)
            {
                Debug.Log(towerPoint);
                var creatingTower = factory.CreateTower(towerPoint.TowerBaseType, towerPoint.WeaponType, towerPoint.Position,
                    towerPoint.Range);
                creatingTowers.Add(creatingTower, new StopWatch(towerPoint.ShootInterval, coroutineRunner));
            }

            foreach (SpawnerPoint spawner in forLevel.SpawnerPoints)
            {
                var creatingMonsterSpawner = factory.CreateSpawner(spawner.Position, spawner.MoveTargetPosition);
                creatingMonsterSpawners.Add(creatingMonsterSpawner, new StopWatchDecoratorWithBool(spawner.Interval, coroutineRunner));
            }

            Debug.Log("smth0");
            Task.WhenAll(creatingMonsterSpawners.Keys);
            Task.WhenAll(creatingTowers.Keys);

            foreach (var monsterStopWatchPair in creatingMonsterSpawners)
            {
                Debug.Log(monsterStopWatchPair.Key + " " + monsterStopWatchPair.Value);
                ConfigureMonsterSpawner(monsterStopWatchPair);
            }
            foreach (var towerStopWatchPair in creatingTowers)
            {
                ConfigureTower(towerStopWatchPair);
            }
        }

        private void ConfigureMonsterSpawner(KeyValuePair<Task<GameObject>, StopWatch> monsterStopWatchPair)
        {
            var spawner = monsterStopWatchPair.Key.Result.GetComponent<ISpawner>();
            StopWatch stopwatch = monsterStopWatchPair.Value;

            stopwatch.OnTime += spawner.Spawn;
            stopwatch.Run();
        }

        private void ConfigureTower(KeyValuePair<Task<GameObject>, StopWatch> towerStopWatchPair)
        {
            var spawner = towerStopWatchPair.Key.Result.GetComponent<ISpawner>();
            var stopwatch = (StopWatchDecoratorWithBool) towerStopWatchPair.Value;
            var observer = towerStopWatchPair.Key.Result.GetComponentInChildren<IObserverInRange>();

            stopwatch.OnTime += spawner.Spawn;
            stopwatch.Run();
            observer.OnMonsterInRangeArea += stopwatch.SetBoolToTrue;
        }

        public void Exit()
        {
            UnsubscribeSpawners();

            UnsubscribeTowers();
            factory.CleanUp();

        }

        private void UnsubscribeTowers()
        {
            foreach (var towerStopWatchPair in creatingTowers)
            {
                var spawner = towerStopWatchPair.Key.Result.GetComponent<ISpawner>();
                var stopwatch = (StopWatchDecoratorWithBool) towerStopWatchPair.Value;
                var observable = towerStopWatchPair.Key.Result.GetComponentInChildren<IObserverInRange>();

                stopwatch.Stop();
                stopwatch.OnTime -= spawner.Spawn;
                observable.OnMonsterInRangeArea -=
                    stopwatch.SetBoolToTrue;
            }
        }

        private void UnsubscribeSpawners()
        {
            foreach (var monsterStopWatchPair in creatingMonsterSpawners)
            {
                var spawner = monsterStopWatchPair.Key.Result.GetComponent<ISpawner>();
                StopWatch stopwatch = monsterStopWatchPair.Value;

                stopwatch.Stop();
                stopwatch.OnTime -= spawner.Spawn;
            }
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState> { }
    }
}