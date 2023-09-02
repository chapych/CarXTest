using UnityEngine;

namespace BaseInterfaces.Gameplay
{
    public interface ISpawner
    {
        float Interval { get; set; }
        GameObject MoveTarget { get; set; }
        void Spawn();
    }
}