using UnityEngine;
using System.Collections;
using Logic;
using Logic.Tower;
using Logic.Tower.Base;

public class CannonProjectile : ProjectileBase
{
	void Update()
	{
		var translation = transform.forward * m_speed;
		transform.Translate(translation);
	}
}
