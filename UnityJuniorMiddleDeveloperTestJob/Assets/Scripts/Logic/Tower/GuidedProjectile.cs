﻿using BaseInterfaces.Gameplay;
using Logic.Tower.Base;
using UnityEngine;

namespace Logic.Tower
{
	public class GuidedProjectile : ProjectileBase, ISpawnableTransform
	{
		private GameObject m_target;
		protected override void Update ()
		{
			base.Update();
			Vector3 translation = m_target.transform.position - transform.position;
			if (translation.magnitude > m_speed) {
				translation = translation.normalized * m_speed;
			}
			transform.Translate(translation);
		}
		public  void SetMovementTarget(Transform target)
		{
			m_target = target.gameObject;
		}
	}
}
