using System.Collections.Generic;
using UnityEngine;

namespace Logic.PoolingSystem
{
    public class Pool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private readonly T prefab;

        private Queue<T> queue = new Queue<T>();

        public Pool(T prefab)
        {
            this.prefab = prefab;
        }

        public T Get()
        {
            if (queue.Count == 0) AddObjects(1);

            T instance = queue.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void ReturnToPool(T instance)
        {
            instance.gameObject.SetActive(false);
            queue.Enqueue(instance);
        }

        public void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T instance = Object.Instantiate(prefab);
                ReturnToPool(instance);

                instance.GetComponent<IPoolable<T>>().OnFree += ReturnToPool;
            }
        }

        public void CleanUp()
        {
            foreach (IPoolable<T> poolable in queue)
            {
                poolable.OnFree -= ReturnToPool;
            }
            queue.Clear();
        }
    }

}