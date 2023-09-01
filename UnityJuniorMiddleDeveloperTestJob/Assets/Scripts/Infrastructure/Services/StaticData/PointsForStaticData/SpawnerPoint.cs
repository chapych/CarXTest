using System;
using UnityEngine;

namespace Infrastructure.Services.StaticData.PointsForStaticData
{
    [Serializable]
    public class SpawnerPoint
    {
        public  Vector3 position;

        public SpawnerPoint(Vector3 position)
        {
            this.position = position;
        }
    }
}