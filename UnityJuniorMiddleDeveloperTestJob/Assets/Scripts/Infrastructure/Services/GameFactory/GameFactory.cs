using System.Collections.Generic;
using System.Threading.Tasks;
using BaseClasses.Enums;
using BaseInterfaces.Gameplay;
using Infrastructure.Services.AssetProviderService;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticDataService;
using Logic;
using Logic.Tower;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService staticDataService;
        private readonly IAssetProvider assetProvider;
        private float height = Constants.HEIGHT;
        private List<TriggerObserver> toUnsubscribe = new List<TriggerObserver>();

        public GameFactory(IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            this.staticDataService = staticDataService;
            this.assetProvider = assetProvider;
        }

        public async Task<GameObject> CreateTower(TowerBaseType towerBaseType, WeaponType weaponType, Vector3 at, float range, float shootInterval)
        {
            Vector3 shift = height * Vector3.up;
            AssetReferenceGameObject towerBaseData = staticDataService.ForTowerBase(towerBaseType).PrefabReference;
            AssetReferenceGameObject weaponData = staticDataService.ForTowerWeapon(weaponType).PrefabReference;

            var towerBasePrefab = await assetProvider.Load<GameObject>(towerBaseData);
            var weaponPrefab = await assetProvider.Load<GameObject>(weaponData);

            GameObject towerBase = Object.Instantiate(towerBasePrefab, at, Quaternion.identity);
            GameObject weapon = InstantiateAsChild(weaponPrefab, towerBase, shift);
            ConstructWeapon(weapon, range, shootInterval);

            return towerBase;
        }

        private static GameObject InstantiateAsChild(GameObject prefab, GameObject parent, Vector3 shift)
        {
            GameObject weapon = Object.Instantiate(prefab, parent.transform, false);
            weapon.transform.localPosition = shift;
            return weapon;
        }

        private void ConstructWeapon(GameObject weapon, float range, float shootInterval)
        {
            var canon = weapon.GetComponent<IObserverInRange>();
            canon.Range = range;
            canon.ShootInterval = shootInterval;

            var triggerObserver = weapon.GetComponentInChildren<TriggerObserver>();
            triggerObserver.Radius = range;
            triggerObserver.OnTrigger += canon.OnInRangeArea;

            toUnsubscribe.Add(triggerObserver);
        }

        public async Task<GameObject> CreateSpawner(Vector3 at, float interval, Vector3 moveTargetPosition)
        {
            var prefab = await assetProvider.Load<GameObject>(AssetAddresses.SPAWNER);
            var spawner = prefab.GetComponent<ISpawner>();
            var target = new GameObject();

            target.transform.position = moveTargetPosition;
            spawner.Interval = interval;
            spawner.MoveTarget = target;

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public void CleanUp()
        {
            UnsubscribeTowerWeapon();
        }

        private void UnsubscribeTowerWeapon()
        {
            foreach (TriggerObserver observer in toUnsubscribe)
            {
                var canon = observer.GetComponent<IObserverInRange>();
                observer.OnTrigger -= canon.OnInRangeArea;
            }
        }
    }
}