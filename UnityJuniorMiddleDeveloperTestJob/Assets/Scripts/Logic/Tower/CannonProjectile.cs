using UnityEngine;
using System.Collections;
using Logic;
using Logic.Tower;

public class CannonProjectile : ProjectileBase {
	public float m_speed = 0.2f;
	public int m_damage = 10;

	void Update () {
		var translation = transform.forward * m_speed;
		transform.Translate (translation);
	}

	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster> ();
		if (monster == null)
			return;

		monster.GetDamage(m_damage);
		Destroy (gameObject);
	}
}
