using BaseInterfaces.Gameplay;
using Logic.Tower.Base;
using UnityEngine;

namespace Logic.Tower
{
	public class CannonProjectile : ProjectileBase, ISpawnableRigidBody
	{
		private Rigidbody rigidbody;
		private void Start()
		{
			rigidbody = GetComponent<Rigidbody>();

		}
		public void SetMovementDirection(Vector3 direction)
		{
			rigidbody.AddForce(m_speed * direction.normalized, ForceMode.VelocityChange);
		}

	}
}
