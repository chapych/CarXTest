using System;
using Logic.PoolingSystem;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(Rigidbody))]
	public class Monster : MonoBehaviour, IPoolable<Monster>
	{
		const float REACH_DISTANCE = 0.3f;

		private GameObject moveTarget;
		private float speed = 0.1f;
		private int maxHP = 30;

		private int m_hp;
		private Rigidbody rigidbody;
		public event Action<Monster> OnFree;

		private void Awake() => rigidbody = GetComponent<Rigidbody>();

		public void Construct(GameObject moveTarget, float speed, int maxHP)
		{
			this.moveTarget = moveTarget;
			this.speed = speed;
			this.maxHP = maxHP;

			m_hp = this.maxHP;
		}

		private void FixedUpdate ()
		{
			if(!moveTarget) return;

			Vector3 moveTargetPosition = moveTarget.transform.position;
			if (IsCloseToTarget())
			{
				OnFree?.Invoke(this);
				return;
			}
			Vector3 newPosition = Vector3.MoveTowards(transform.position, moveTargetPosition, speed);
			rigidbody.MovePosition(newPosition);

			bool IsCloseToTarget() => Vector3.Distance (transform.position, moveTargetPosition) <= REACH_DISTANCE;
		}

		public void GetDamage(int amount)
		{
			m_hp -= amount;
			if (m_hp <= 0) OnFree?.Invoke(this);
		}
	}
}
