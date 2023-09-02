using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseClasses.Enums;
using Infrastructure.Services.AssetProviderService;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticDataService.StaticData;
using UnityEngine;

namespace Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider assetProvider;

        private Dictionary<string, LevelStaticData> levelsByKey;
        private Dictionary<TowerBaseType, TowerBaseStaticData> towerBasesByKey;
        private Dictionary<WeaponType, TowerWeaponStaticData> towerWeaponByKey;

        private string staticDataKey = "StaticData";

        public StaticDataService(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async Task Load()
        {
            assetProvider.CleanUp();

            var levels = await assetProvider.LoadAllByKey<LevelStaticData>(staticDataKey);
            var towerBases = await assetProvider.LoadAllByKey<TowerBaseStaticData>(staticDataKey);
            var towerWeapons = await assetProvider.LoadAllByKey<TowerWeaponStaticData>(staticDataKey);

            levelsByKey = levels.ToDictionary(x => x.LevelName, x => x);
            towerBasesByKey = towerBases.ToDictionary(x => x.Type, x => x);
            towerWeaponByKey = towerWeapons.ToDictionary(x => x.Type, x => x);
        }

        public LevelStaticData ForLevel(string level) => levelsByKey[level];
        public TowerBaseStaticData ForTowerBase(TowerBaseType type) => towerBasesByKey[type];
        public TowerWeaponStaticData ForTowerWeapon(WeaponType type) => towerWeaponByKey[type];
    }
}


