using UnityEngine;
using System.Collections;
using Logic;

public class CannonTower : TowerBase
{
	public Transform m_shootPoint;

	protected override void Shoot(GameObject target)
	{
		Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
	}
}
