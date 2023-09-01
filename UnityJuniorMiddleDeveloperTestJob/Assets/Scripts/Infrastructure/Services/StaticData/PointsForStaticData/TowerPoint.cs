using System;
using UnityEngine;

namespace Infrastructure.Services.StaticData.PointsForStaticData
{
    [Serializable]
    public class TowerPoint
    {
        public Vector3 position;
        public TowerBaseType towerBaseType;
        public WeaponType weaponType;

        public TowerPoint(Vector3 position, TowerBaseType towerBaseType, WeaponType weaponType)
        {
            this.weaponType = weaponType;
            this.towerBaseType = towerBaseType;
            this.position = position;
        }
    }
}