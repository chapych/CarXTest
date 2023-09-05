using Logic.PoolingSystem;
using UnityEngine;

namespace Logic.Tower.Base
{
    public abstract class TowerBase : MonoBehaviour, IObserverInRange
    {
        [SerializeField] protected ProjectileBase m_projectilePrefab;
        protected Pool<ProjectileBase> projectilePool;
        protected float speed;
        protected int damage;

        private StopWatch stopWatch;

        public void Construct(float interval, float speed, int damage)
        {
            stopWatch = new StopWatch(interval);
            this.speed = speed;
            this.damage = damage;

            projectilePool = new Pool<ProjectileBase>(m_projectilePrefab);
        }

        private void Start() => projectilePool.AddObjects(5);
        public void OnInRangeArea(GameObject observable)
        {
            if(observable.TryGetComponent(out IDamageable damageable))
                if(stopWatch.IsTimeForPeriodicAction()) Shoot(damageable);
        }

        protected abstract void Shoot(IDamageable target);
    }
}