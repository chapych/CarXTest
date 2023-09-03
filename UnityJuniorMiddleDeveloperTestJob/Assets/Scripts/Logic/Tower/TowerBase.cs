using Logic.PoolingSystem;
using Logic.Tower;
using UnityEngine;

namespace Logic
{
    public abstract class TowerBase : MonoBehaviour, IObserverInRange
    {
        [SerializeField] protected ProjectileBase m_projectilePrefab;
        private float m_lastShotTime = -0.5f;
        private float nextShot;
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
            if(stopWatch.IsTimeForPeriodicAction()) Shoot(observable);
        }

        protected abstract void Shoot(GameObject target);
    }
}