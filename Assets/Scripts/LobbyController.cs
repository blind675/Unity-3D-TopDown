using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour {

	public Text bricksLabel;
	public Text coinsLabel;
	public PlayerData playerData;
	public AudioClip [] yaaayAudioClips;

	// Start is called before the first frame update
	void Start ()
	{
		bricksLabel.text = "Bricks X " + playerData.bricksInInventory + " = ";
		coinsLabel.text = playerData.bricksInInventory + " Coins";

		playerData.coins += playerData.bricksInInventory;
		playerData.bricksInInventory = 0;

		Invoke ("PlayYaaaySound", 0.25f);
	}

	public void CloseLobby ()
	{
		SceneController.NextScene ();
	}

	private void PlayYaaaySound ()
	{
		// get a random audio clip
		int randomIndex = Random.Range (0, yaaayAudioClips.Length);
		AudioClip randomAudioClip = yaaayAudioClips [randomIndex];

		// play audio clip
		AudioSource.PlayClipAtPoint (randomAudioClip, gameObject.transform.position);
	}

}
