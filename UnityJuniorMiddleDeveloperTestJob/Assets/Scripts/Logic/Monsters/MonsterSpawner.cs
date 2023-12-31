﻿using System;
using System.Threading.Tasks;
using BaseClasses.Enums;
using BaseInterfaces.Gameplay;
using Extensions;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.StaticDataService.StaticData;
using Logic.PoolingSystem;
using Logic.Tower;
using UnityEngine;

namespace Logic
{
	public class MonsterSpawner : MonoBehaviour, ISpawner
	{
		private Vector3 moveTargetPosition;
		private IGameFactory factory;

		private MonsterType monsterType;
		private Pool pool;
		private int preCreatedNumber = 5;
		private GameObject target;

		public void Construct(Vector3 moveTargetPosition, IGameFactory factory)
		{
			this.moveTargetPosition = moveTargetPosition;
			this.factory = factory;

			var instantiatingFunc = new Func<Task<GameObject>>(() => factory.CreateMonster(monsterType));

			pool = new Pool(instantiatingFunc);
		}

		private void Start()
		{
			pool.AddObjects(preCreatedNumber);
			target = CreateTarget();
		}

		public void Spawn()
		{
			var monsterMove = (MonsterMove) pool.Get();
			Vector3 direction = target.transform.position - transform.position;

			monsterMove.Target = target;
			monsterMove.SetMovementDirection(direction.WithY(0f));
		}

		private GameObject CreateTarget()
		{
			moveTargetPosition = new Vector3(moveTargetPosition.x,
				0,
				moveTargetPosition.z);
			return factory.CreateTarget(moveTargetPosition);
		}

	}
}
