using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	public static GameObject brickInContact = null;

	public PlayerController player;
	public GameObject pickupBricksButton;

	public void Shoot ()
	{
		player.Shoot ();
	}

	public void PickUp ()
	{
		player.PickUpBrick ();
	}

	// TODO: pick up brick on button for mobile ??
	//private void Update ()
	//{
	//	if (brickInContact && !pickupBricksButton.activeSelf) {
	//		pickupBricksButton.SetActive (true);
	//	} else if (!brickInContact && pickupBricksButton.activeSelf) {
	//		pickupBricksButton.SetActive (false);
	//	}
	//}
}
