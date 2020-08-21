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
	private EnemyHitController enemyHitController;

	private void Start ()
	{
		enemyHitController = GetComponent<EnemyHitController> ();
		healthBar.SetMaxhealth (maxLife);
		life = maxLife;
	}

	private void OnCollisionEnter (Collision collision)
	{
		// TODO: extract in metod
		if (collision.gameObject.tag == "Brick") {

			// process collision data
			float lifeDecrease = enemyHitController.GetLifeDecreaseForCollision (collision);

			if (lifeDecrease == 0) {
				return;
			}

			// update life value
			life -= lifeDecrease - armour;

			// Did it die ?
			if (life < 0) {
				// TODO: play die sound
				// TODO: die FX
				Destroy (gameObject);
			}

			// take care of visuals and FX
			UpdateHealthBar (life);
			enemyHitController.ShowFXForCollision (collision);
			// and audio
			enemyHitController.PlaySoundFXForCollision (collision);
		}

	}

	private void UpdateHealthBar (float life)
	{
		healthBar.SetHealth (life);
	}



}
