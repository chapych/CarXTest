using System;
using Logic.PoolingSystem;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(Rigidbody))]
	public class Monster : MonoBehaviour, IPoolable<Monster>, IDamageable
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
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;

				OnFree?.Invoke(this);
			}

			bool IsCloseToTarget() => Vector3.Distance (transform.position, moveTargetPosition) <= REACH_DISTANCE;
		}

		public void SetMovement()
		{
			Vector3 moveDirection = moveTarget.transform.position - transform.position;
			rigidbody.AddForce(speed * moveDirection.normalized, ForceMode.VelocityChange);
		}

		public bool IsDead()
		{
			return m_hp <= 0;
		}

		public void GetDamage(int amount)
		{
			m_hp -= amount;
			if (m_hp <= 0) OnFree?.Invoke(this);
		}
	}
}
