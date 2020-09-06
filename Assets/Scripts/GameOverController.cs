using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

	public PlayerData playerData;
	public AudioClip [] boooAudioClips;

	private void Start ()
	{
		playerData.bricksInInventory = 0;

		//Invoke ("PlayBoooSound", 0.25f);
	}

	public void GoToLobby ()
	{
		SceneController.GoToMenuScene ();
	}

	private void PlayBoooSound ()
	{
		// get a random audio clip
		int randomIndex = Random.Range (0, boooAudioClips.Length);
		AudioClip randomAudioClip = boooAudioClips [randomIndex];

		// play audio clip
		AudioSource.PlayClipAtPoint (randomAudioClip, gameObject.transform.position);
	}
}
