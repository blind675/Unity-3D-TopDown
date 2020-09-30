using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFXController : MonoBehaviour {
	public AudioClip [] hitAudioClips;
	public CameraShake cameraShake;

	public void ShowFXForCollision (Collision collision)
	{
		ShakeScreen ();
	}

	private void ShakeScreen ()
	{
		StartCoroutine (cameraShake.Shake (0.10f, 0.5f));
	}

	public void PlaySoundFXForHit ()
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
