using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitController : AbstractHitController {

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
		healthBar.SetMaxHealth (enemyStats.maxLife);

		enemyArmour = enemyStats.armour;
	}

	override public void DecreaseTargetLifeWithValue (float lifeDecrease)
	{
		life -= lifeDecrease - enemyArmour;
	}

	override public bool DidTargetDie () => life < 0;

	override public void HandleTargetDied ()
	{
		// play die sound
		enemyFXController.PlaySoundFXForDestroy ();
		// die FX
		enemyFXController.ShowDestroyFX ();
	}

	override public void HandleTargetRecievedHit (Collision collision)
	{
		// take care of visuals and FX
		healthBar.SetHealth (life);
		enemyFXController.ShowFXForCollision (collision);
		// and audio
		enemyFXController.PlaySoundFXForHit ();
	}

	override public void BrickCollisionEnter (Collision collision) { }

	override public void BrickCollisionExit (Collision collision) { }
}
