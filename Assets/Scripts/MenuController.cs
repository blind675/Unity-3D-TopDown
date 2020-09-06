using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class MenuController : MonoBehaviour {
	public Dropdown dropdown;

	public PlayerData playerData;

	private int higestLevelReached;
	private int selectedLevel;

	// Start is called before the first frame update
	void Start ()
	{
		higestLevelReached = LevelManager.highestLevelIndex;

		// set the new dropdown values
		dropdown.ClearOptions ();
		for (int i = 0; i < higestLevelReached; i++) {
			dropdown.options.Add (new OptionData ((i + 1).ToString ()));
		}

		dropdown.options.Add (new OptionData ("Next"));

		dropdown.value = higestLevelReached + 1;
	}

	public void DropDownValueChanged (int newValue)
	{
		selectedLevel = newValue;
	}

	public void PlayButtonPressed ()
	{
		SceneController.LoadLevelSceneForLevel (selectedLevel);
	}

}
