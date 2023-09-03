using UnityEngine;
using System.Collections;
using Logic;
using Logic.Tower;

public class GuidedProjectile : ProjectileBase {
	public GameObject m_target;


	private void Start()
	{
		if (!m_target) OnFreeAction(this);
	}
	private void Update ()
	{
		Vector3 translation = m_target.transform.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate(translation);
	}
}
