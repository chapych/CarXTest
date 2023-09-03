using System;
using Logic.PoolingSystem;
using UnityEngine;

namespace Logic.Tower
{
    public class ProjectileBase : MonoBehaviour, IPoolable<ProjectileBase>
    {
        private TriggerObserver triggerObserver;
        public float m_speed = 0.2f;
        public int m_damage = 10;
        public event Action<ProjectileBase> OnFree;

        private void Start()
        {
            triggerObserver = GetComponent<TriggerObserver>();
            triggerObserver.OnTrigger += OnTriggerHandle;
        }

        private void OnTriggerHandle(GameObject other)
        {
            if(!other.TryGetComponent(out Monster monster))
                return;

            monster.GetDamage(m_damage);
            OnFree?.Invoke(this);
        }

        protected void OnFreeAction(ProjectileBase projectileBase) => OnFree?.Invoke(projectileBase);

        private void OnDestroy()
        {
            triggerObserver.OnTrigger -= OnTriggerHandle;
        }
    }
}