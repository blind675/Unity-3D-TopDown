using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitController : MonoBehaviour {

	const float IMPACT_FORCE_TRESHOLD = 200f;
	const float IMPACT_FORCE_MAX = 700f;

	public HealthBarScript healthBar;

	private float enemyArmour;
	private float life;

	private EnemyFXController enemyFXController;
	private EnemyStats enemyStats;

	private void Start ()
	{
		enemyStats = GetComponent<EnemyStats> ();
		enemyFXController = GetComponent<EnemyFXController> ();

		life = enemyStats.maxLife;
		healthBar.SetMaxhealth (enemyStats.maxLife);

		enemyArmour = enemyStats.armour;
	}

	private void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Brick") {

			// process collision data
			float lifeDecrease = GetLifeDecreaseForCollision (collision);

			if (lifeDecrease == 0) {
				// weak hit
				return;
			}

			UpdateLifeValueWithDecrease (lifeDecrease);

			if (DidEnemyDie ()) {
				// play die sound
				enemyFXController.PlaySoundFXForDestroy ();
				// die FX
				enemyFXController.ShowDestroyFX ();

			} else {
				// take care of visuals and FX
				UpdateHealthBar (life);
				enemyFXController.ShowFXForCollision (collision);
				// and audio
				enemyFXController.PlaySoundFXForCollision (collision);
			}
		}

	}

	// TODO: change to get more punch or make weaker enemyes ...
	private float GetLifeDecreaseForCollision (Collision collision)
	{
		float impactForce = GetImpactForceForCollision (collision);
		impactForce = Mathf.Clamp (impactForce, IMPACT_FORCE_TRESHOLD, IMPACT_FORCE_MAX);

		float lifeDecreaseValue = impactForce.Map (IMPACT_FORCE_TRESHOLD, IMPACT_FORCE_MAX, 0, 40);

		return lifeDecreaseValue;
	}

	private float GetImpactForceForCollision (Collision collision) => collision.impulse.magnitude / Time.fixedDeltaTime;

	private void UpdateLifeValueWithDecrease (float lifeDecrease)
	{
		life -= lifeDecrease - enemyArmour;
	}

	private bool DidEnemyDie () => life < 0;

	private void UpdateHealthBar (float life)
	{
		healthBar.SetHealth (life);
	}

}

// TODO: some kind of utils class
public static class ExtensionMethods {

	public static float Map (this float value, float fromSource, float toSource, float fromTarget, float toTarget)
	{
		return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
	}

}
