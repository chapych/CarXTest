using System.Diagnostics.CodeAnalysis;
using BaseInterfaces.Gameplay;
using Logic.PoolingSystem;
using UnityEngine;

namespace Logic
{
	public class Spawner : MonoBehaviour, ISpawner
	{
		private Vector3 moveTargetPosition;
		private int maxHP;
		private float speed;
		private float nextShot;

		[SerializeField] private Monster monsterPrefab;
		private GameObject moveTarget;
		private Pool<Monster> pool;
		private StopWatch stopWatch;

		public void Construct(float interval, Vector3 moveTargetPosition, float speed, int maxHP)
		{
			this.moveTargetPosition = moveTargetPosition;
			this.speed = speed;
			this.maxHP = maxHP;

			pool = new Pool<Monster>(monsterPrefab);
			stopWatch = new StopWatch(interval);
		}

		public void WarmUp() => pool.AddObjects(1);

		private void Update()
		{
			if(stopWatch.IsTimeForPeriodicAction()) Spawn();
		}

		public void Spawn()
		{
			Monster newMonster = pool.Get();
			if (!moveTarget)
				moveTarget = CreateTargetFor(newMonster);
			ConfigureMonster(newMonster, moveTarget);
		}

		private void ConfigureMonster(Monster newMonster, GameObject target)
		{
			newMonster.Construct(target, speed, maxHP);
			newMonster.transform.position = transform.position;
			Debug.Log(newMonster.GetComponent<Rigidbody>().velocity);
		}

		private GameObject CreateTargetFor(Monster monster)
		{
			moveTarget = new GameObject("Move Target");
			var aim = new Vector3(moveTargetPosition.x, monster.GetComponent<Collider>().bounds.size.y/2, moveTargetPosition.z);
			moveTarget.transform.position = aim;
			return moveTarget;
		}
	}
}
