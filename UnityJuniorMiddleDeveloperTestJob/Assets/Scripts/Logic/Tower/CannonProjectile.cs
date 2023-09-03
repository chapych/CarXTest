using UnityEngine;
using System.Collections;
using Logic;
using Logic.Tower;

public class CannonProjectile : ProjectileBase
{
	void Update()
	{
		var translation = transform.forward * m_speed;
		transform.Translate(translation);
	}
}
