using UnityEngine;
using System.Collections;
using Logic;
using Logic.Tower;

public class CannonTower : TowerBase
{
	public Transform m_shootPoint;

	protected override void Shoot(GameObject target)
	{
		var projectile = pool.Get() as CannonProjectile;

		Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
	}
}
