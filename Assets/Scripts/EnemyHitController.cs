using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitController : MonoBehaviour {

	public GameObject bloodSplahsFX;
	public AudioClip [] hitAudioClips;

	public float GetLifeDecreaseForCollision (Collision collision)
	{
		float impactForce = GetImpactForceForCollision (collision);

		//FIXME: 300 and 15 are chosen randomly ...put in constants ?
		if (impactForce > 300) {
			return (impactForce / 15);
		} else {
			return 0;
		}
	}

	public void ShowFXForCollision (Collision collision)
	{
		// show splash
		bloodSplahsFX.transform.position = collision.GetContact (0).point;
		bloodSplahsFX.SetActive (true);

		// hide after 0.5 seconds
		StartCoroutine (HideFX ());
	}

	IEnumerator HideFX ()
	{
		yield return new WaitForSeconds (0.5f);

		bloodSplahsFX.SetActive (false);
	}

	public void PlaySoundFXForCollision (Collision collision)
	{
		// get a random audio clip
		int randomIndex = Random.Range (0, hitAudioClips.Length);
		AudioClip randomHitAudioClip = hitAudioClips [randomIndex];

		// play hit sound
		AudioSource.PlayClipAtPoint (randomHitAudioClip, collision.gameObject.transform.position);

	}

	private float GetImpactForceForCollision (Collision collision) => collision.impulse.magnitude / Time.fixedDeltaTime;
}
