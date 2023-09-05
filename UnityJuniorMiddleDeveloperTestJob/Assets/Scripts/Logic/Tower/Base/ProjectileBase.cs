using System;
using Logic.PoolingSystem;
using UnityEngine;

namespace Logic.Tower.Base
{
    public class ProjectileBase : MonoBehaviour, IPoolable<ProjectileBase>
    {
        private TriggerObserver triggerObserver;
        protected float m_speed;
        protected int m_damage;
        protected IDamageable m_target;
        public event Action<ProjectileBase> OnFree;

        public void Construct(float speed, int damage, IDamageable target)
        {
            m_speed = speed;
            m_damage = damage;
            m_target = target;
        }
        
        private void Awake()
        {
            triggerObserver = GetComponent<TriggerObserver>();
            triggerObserver.OnTrigger += OnTriggerHandle;
        }

        protected virtual void Update()
        {
            if (m_target.IsDead())
            {
                OnFreeAction(this);
            }
        }

        private void OnTriggerHandle(GameObject other)
        {
            if(!other.TryGetComponent(out IDamageable monster))
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