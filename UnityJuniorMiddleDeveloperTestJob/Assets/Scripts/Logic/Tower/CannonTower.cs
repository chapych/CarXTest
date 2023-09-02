using UnityEngine;
using System.Collections;
using Logic;

public class CannonTower : MonoBehaviour, IObserverInRange {
	public float ShootInterval { get; set; } = 0.5f;
	public float Range { get; set; } = 4f;
	public GameObject m_projectilePrefab;
	public Transform m_shootPoint;

	private float nextShot;

	public void OnInRangeArea(GameObject observable)
	{
		if(Time.time < nextShot)
			return;
		nextShot += ShootInterval;
		Shot(observable);
	}

	private void Shot(GameObject monster)
	{
		Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
	}
}
