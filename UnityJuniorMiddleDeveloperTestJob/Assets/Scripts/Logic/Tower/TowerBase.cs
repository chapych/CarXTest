using Logic.PoolingSystem;
using Logic.Tower;
using UnityEngine;

namespace Logic
{
    public abstract class TowerBase : MonoBehaviour, IObserverInRange
    {
        [SerializeField] protected GameObject m_projectilePrefab;
        private float m_lastShotTime = -0.5f;
        private float nextShot;
        private StopWatch stopWatch;
        private Pool<ProjectileBase> pool;

        public void Construct(float interval)
        {
            stopWatch = new StopWatch(interval);

        }

        public void OnInRangeArea(GameObject observable)
        {
            if(stopWatch.IsTimeForPeriodicAction()) Shoot(observable);
        }

        protected abstract void Shoot(GameObject target);
    }
}