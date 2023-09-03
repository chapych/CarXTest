using System;
using Logic.Tower;
using UnityEngine;

namespace Logic.PoolingSystem
{
    public interface IPoolable<T>
    {
        public event Action<T> OnFree;
    }
}