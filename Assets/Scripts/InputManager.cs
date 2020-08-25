using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	[SerializeReference]
	private PlayerController player;

	public void Shoot ()
	{
		player.Shoot ();
	}

	public void CloseLobby ()
	{
		SceneController.NextScene ();
	}
}
