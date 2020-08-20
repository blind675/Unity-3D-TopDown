using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	private float maxLife = 100;
	[SerializeField]
	private float armour = 10;

	public HealthBarScript healthBar;

	private float life;

	private void Start ()
	{
		healthBar.SetMaxhealth (maxLife);
		life = maxLife;
	}

	private void OnCollisionEnter (Collision collision)
	{
		// TODO: extract in metod
		if (collision.gameObject.tag == "Brick") {
			float impactForce = GetImpactForceForCollision (collision);

			if (impactForce > 300) {
				life -= (impactForce / 15) - armour;
				healthBar.SetHealth (life);

			}

			if (life < 0) {
				Destroy (gameObject);
			}
		}

	}

	private float GetImpactForceForCollision (Collision collision) => collision.impulse.magnitude / Time.fixedDeltaTime;

}
