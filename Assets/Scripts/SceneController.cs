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

		// Load Lobby Scene
		SceneManager.LoadScene ("LobbyScene");
	}

	public static void NextScene ()
	{
		if (LevelManager.ShouldLoadTutorialLevel ()) {
			// Load Next Tutorial Level

			levelEnded = false;
			SceneManager.LoadScene ("GameLevelScene");
		} else {
			// Load Menu Scene
			SceneManager.LoadScene ("MenuScene");

		}
	}

	public static void LoadLevelSceneForLevel (int level)
	{
		levelEnded = false;
		LevelManager.LoadLevel (level);
		SceneManager.LoadScene ("GameLevelScene");
	}
}
