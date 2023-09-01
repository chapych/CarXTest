using System.Collections.Generic;
using Infrastructure.BaseClasses.OnScenePoints;
using Infrastructure.Services.StaticData.PointsForStaticData;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "New Level Static Data", menuName = "Static Data/Level Static Data")]
    public class LevelStaticData : ScriptableObject
    {
        public string levelName;
        public List<TowerPoint> towerPoints;
        public List<SpawnerPoint> spawnerPoints;
    }
}