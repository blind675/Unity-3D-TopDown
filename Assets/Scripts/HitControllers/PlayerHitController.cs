using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : AbstractHitController {

	public HealthBarScript healthBar;
	public PlayerData playerData;

	private float armour;
	private float life;

	// Start is called before the first frame update
	void Start ()
	{
		life = playerData.health;

		if (healthBar) {
			healthBar.SetMaxHealth (life);
		}

		armour = playerData.armour;
	}

	public override void DecreaseTargetLifeWithValue (float lifeDecrease)
	{
		life -= lifeDecrease - armour;
	}

	public override bool DidTargetDie () => life < 0;

	public override void HandleTargetDied ()
	{
		////TODO: play die sound
		//playerFXController.PlaySoundFXForDestroy ();

		// show game over screen
		SceneController.GoToGameOverScene ();
	}

	public override void HandleTargetRecievedHit (Collision collision)
	{
		// take care of visuals and FX
		if (healthBar) {
			healthBar.SetHealth (life);
		}

		// TODO:
		//playerFXController.ShowFXForCollision (collision);
		//// and audio
		//playerFXController.PlaySoundFXForHit ();
	}

	override public void BrickCollisionEnter (Collision collision)
	{
		InputManager.brickInContact = collision.gameObject;
	}

	override public void BrickCollisionExit (Collision collision)
	{
		InputManager.brickInContact = null;
	}
}
