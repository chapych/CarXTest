using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "New Tower Base Static Data", menuName = "Static Data/Tower Base Static Data")]
    public class TowerBaseStaticData : ScriptableObject
    {
        public TowerBaseType type;
        public GameObject prefab;
    }
}