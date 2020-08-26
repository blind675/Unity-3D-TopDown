using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager {

	static private Texture2D [] levels;
	static private Texture2D [] tutorialLevels;

	static private int tutorialLevelIndex = 0;
	static private int levelIndex = 0;

	static public int highestLevelIndex = 0;

	static public void LoadTutorialLevels ()
	{
		tutorialLevels = Resources.LoadAll<Texture2D> ("Levels/Tutorial");
		tutorialLevelIndex = 0;
	}

	static public void LoadLevels ()
	{
		levels = Resources.LoadAll<Texture2D> ("Levels/Game");
		levelIndex = 0;
	}

	static public Texture2D GetLevelImageMap ()
	{
		if (tutorialLevels == null) {
			LoadTutorialLevels ();
		}

		if (tutorialLevelIndex == tutorialLevels.Length) {
			if (levels == null) {
				LoadLevels ();
			}

			levelIndex = Mathf.Clamp (levelIndex, 0, levels.Length - 1);
			return levels [levelIndex];
		}

		return tutorialLevels [tutorialLevelIndex];
	}

	static public void GoToNextLeve ()
	{
		if (tutorialLevelIndex < tutorialLevels.Length) {
			tutorialLevelIndex++;
		} else {
			levelIndex++;
			levelIndex = Mathf.Clamp (levelIndex, 0, levels.Length);
			highestLevelIndex = levelIndex;
		}
	}

	static public bool ShouldLoadTutorialLevel () => tutorialLevelIndex < tutorialLevels.Length;

	static public bool IsFirstTutorialLevel () => tutorialLevelIndex == 0;

	static public int LevelsCount ()
	{
		if (levels == null) {
			LoadLevels ();
		}

		return levels.Length;
	}

	static public bool CanLoadLevel (int level) => level <= highestLevelIndex;

	static public void LoadLevel (int level)
	{
		if (CanLoadLevel (level)) {
			levelIndex = level;
		} else {
			levelIndex = highestLevelIndex;
		}
	}

}
