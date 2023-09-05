using Logic.Tower.Base;
using UnityEngine;

namespace Logic.Tower
{
	public class CannonTower : TowerBase
	{
		[SerializeField] private Transform m_shootPoint;

		protected override void Shoot(IDamageable target)
		{
			ProjectileBase projectile = pool.Get();

			Transform projectileTransform = projectile.transform;
			projectileTransform.position = m_shootPoint.position;
			projectileTransform.rotation = m_shootPoint.rotation;


		}
	}
}
