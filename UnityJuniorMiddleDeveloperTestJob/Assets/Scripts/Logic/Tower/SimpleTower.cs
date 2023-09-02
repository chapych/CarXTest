using UnityEngine;

namespace Logic
{
	public class SimpleTower : MonoBehaviour, IObserverInRange
	{
		public float ShootInterval { get; set; } = 0.5f;
		public float Range { get; set; } = 4f;

		public GameObject m_projectilePrefab;

		private float m_lastShotTime = -0.5f;
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
			GameObject projectile =
				Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
			var projectileBeh = projectile.GetComponent<GuidedProjectile>();
			projectileBeh.m_target = monster;
		}
	}
}
