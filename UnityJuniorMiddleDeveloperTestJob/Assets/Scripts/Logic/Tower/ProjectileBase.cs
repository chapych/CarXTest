using System;
using Logic.PoolingSystem;
using UnityEngine;

namespace Logic.Tower
{
    public class ProjectileBase : MonoBehaviour, IPoolable<ProjectileBase>
    {
        public event Action<ProjectileBase> OnFree;
    }
}