using UnityEngine;

namespace Logic
{
    public interface IObserverInRange
    {
        float ShootInterval { get; set; }
        float Range { get; set; }
        void OnInRangeArea(GameObject observable);
    }
}