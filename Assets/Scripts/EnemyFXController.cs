using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CubeExplosionFX))]
public class EnemyFXController : MonoBehaviour {

	public GameObject bloodSplahsFX;
	public AudioClip [] hitAudioClips;
	public AudioClip [] destroyAudioClips;

	private CubeExplosionFX explodeFX;

	private void Start ()
	{
		explodeFX = GetComponent<CubeExplosionFX> ();
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

	public void ShowDestroyFX ()
	{
		explodeFX.Explode ();
	}

	public void PlaySoundFXForDestroy ()
	{
		PlayRandomAudioClipFromArray (destroyAudioClips);
	}

	public void PlaySoundFXForCollision (Collision collision)
	{
		PlayRandomAudioClipFromArray (hitAudioClips);
	}

	private void PlayRandomAudioClipFromArray (AudioClip [] audioClips)
	{
		// get a random audio clip
		int randomIndex = Random.Range (0, audioClips.Length);
		AudioClip randomAudioClip = audioClips [randomIndex];

		// play audio clip
		AudioSource.PlayClipAtPoint (randomAudioClip, gameObject.transform.position);
	}
}
