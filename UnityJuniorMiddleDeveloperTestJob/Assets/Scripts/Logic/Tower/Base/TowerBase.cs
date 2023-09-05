using Logic.PoolingSystem;
using UnityEngine;

namespace Logic.Tower.Base
{
    public abstract class TowerBase : MonoBehaviour, IObserverInRange
    {
        [SerializeField] protected ProjectileBase m_projectilePrefab;
        private StopWatch stopWatch;
        protected Pool<ProjectileBase> pool;

        public void Construct(float interval)
        {
            stopWatch = new StopWatch(interval);

            pool = new Pool<ProjectileBase>(m_projectilePrefab);
        }

        private void Start() => pool.AddObjects(5);
        public void OnInRangeArea(GameObject observable)
        {
            if(observable.TryGetComponent(out IDamageable damageable))
                if(stopWatch.IsTimeForPeriodicAction()) Shoot(damageable);
        }

        protected abstract void Shoot(IDamageable target);
    }
}