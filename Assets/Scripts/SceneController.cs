using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController {

	public static bool levelEnded = false;

	public static void EndLevel ()
	{
		if (levelEnded) {
			return;
		}

		LevelManager.GoToNextLeve ();

		levelEnded = true;
		Debug.Log ("- Go To Lobby Scene");

		// Load Lobby Scene
		SceneManager.LoadScene ("LobbyScene");
	}

	public static void NextScene ()
	{
		if (LevelManager.ShouldLoadTutorialLevel ()) {
			// Load Next Tutorial Level

			levelEnded = false;
			Debug.Log ("- Go To Game Level Scene");

			SceneManager.LoadScene ("GameLevelScene");
		} else {

			Debug.Log ("- Go To Menu Scene");

			// Load Menu Scene
			//SceneManager.LoadScene ("MenuScene");
			// FIXME: remove this
			SceneManager.LoadScene ("GameLevelScene");
		}
	}
}
