using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "New Tower Weapon Static Data", menuName = "Static Data/Tower Weapon Static Data")]
    public class TowerWeaponStaticData : ScriptableObject
    {
        public WeaponType type;
        public GameObject prefab;
    }
}