using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbstractHitController : MonoBehaviour {

	const float IMPACT_FORCE_TRESHOLD = 150f;
	const float IMPACT_FORCE_MAX = 600f;

	public abstract void BrickCollisionEnter (Collision collision);
	public abstract void BrickCollisionExit (Collision collision);

	public abstract void DecreaseTargetLifeWithValue (float lifeDecrease);
	public abstract bool DidTargetDie ();
	public abstract void HandleTargetDied ();
	public abstract void HandleTargetRecievedHit (Collision collision);

	private void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Brick") {

			BrickCollisionEnter (collision);

			// process collision data
			float lifeDecrease = GetLifeDecreaseForCollision (collision);

			if (lifeDecrease == 0) {
				// weak hit
				return;
			}

			DecreaseTargetLifeWithValue (lifeDecrease);

			if (DidTargetDie ()) {
				HandleTargetDied ();
			} else {
				HandleTargetRecievedHit (collision);
			}
		}

	}

	private void OnCollisionExit (Collision collision)
	{
		if (collision.gameObject.tag == "Brick") {
			BrickCollisionExit (collision);
		}
	}

	// TODO: change to get more punch or make weaker enemyes ...
	private float GetLifeDecreaseForCollision (Collision collision)
	{
		float impactForce = GetImpactForceForCollision (collision);
		impactForce = Mathf.Clamp (impactForce, IMPACT_FORCE_TRESHOLD, IMPACT_FORCE_MAX);
		float lifeDecreaseValue = impactForce.Map (IMPACT_FORCE_TRESHOLD, IMPACT_FORCE_MAX, 0, 60);

		return lifeDecreaseValue;
	}

	private float GetImpactForceForCollision (Collision collision) => collision.impulse.magnitude / Time.fixedDeltaTime;

}

// TODO: some kind of utils class
public static class ExtensionMethods {

	public static float Map (this float value, float fromSource, float toSource, float fromTarget, float toTarget)
	{
		return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
	}

}
