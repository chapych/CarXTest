using UnityEngine;

namespace Logic
{
	public class SimpleTower : TowerBase
	{
		protected override void Shoot(GameObject target)
		{
			GameObject projectile =
				Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
			var projectileBeh = projectile.GetComponent<GuidedProjectile>();
			projectileBeh.m_target = target;
		}
	}
}
