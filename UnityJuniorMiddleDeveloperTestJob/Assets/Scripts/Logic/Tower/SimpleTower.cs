using Logic.Tower;
using UnityEngine;

namespace Logic
{
	public class SimpleTower : TowerBase
	{
		protected override void Shoot(GameObject target)
		{
			// GameObject projectile =
			// 	Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
			// var projectileBeh = projectile.GetComponent<GuidedProjectile>();
			var projectile = pool.Get() as GuidedProjectile;
			if (projectile is { })
			{
				projectile.transform.position = transform.position + Vector3.up * 1.5f;

				projectile.m_target = target;
			}
		}
	}
}
