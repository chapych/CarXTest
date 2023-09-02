using BaseInterfaces.Gameplay;
using UnityEngine;

namespace Logic
{
	public class Spawner : MonoBehaviour, ISpawner
	{
		public float Interval { get; set; } = 3;
		public GameObject MoveTarget { get; set; }

		private float m_lastSpawn = -1;
		private float nextShot;
		[SerializeField] private Monster monsterPrefab;

		void Update () {
			if(Time.time < nextShot)
				return;
			nextShot += Interval;
			Spawn();
		}

		public void Spawn()
		{
			Monster newMonster = Object.Instantiate(monsterPrefab);
			ConfigureMonster(newMonster);
		}

		private void ConfigureMonster(Monster monster)
		{
			monster.transform.position = transform.position;
			monster.m_moveTarget = MoveTarget;
		}
	}
}
