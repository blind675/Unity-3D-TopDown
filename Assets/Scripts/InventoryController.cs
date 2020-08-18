using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

	public static int bricksCount = 5;
	public GameObject [] bricks;

	private void Start ()
	{
		// activate the bricksCount bricks and hide the rest
		for (int i = 0; i < bricks.Length; i++) {
			if (i < bricksCount) {
				bricks [i].SetActive (true);
			} else {
				bricks [i].SetActive (false);
			}
		}
	}

	public bool CanPickUpMoreBricks ()
	{
		return bricksCount < bricks.Length;
	}

	public void AddBrick (GameObject brickToAdd)
	{
		if (CanPickUpMoreBricks ()) {
			ActivateTheNextBrickInIventory ();
			DestroyTheBrickFromTheGround (brickToAdd);
		}
	}

	private void ActivateTheNextBrickInIventory ()
	{
		bricksCount++;
		bricks [bricksCount].SetActive (true);
	}

	private void DestroyTheBrickFromTheGround (GameObject brickToAdd)
	{
		Destroy (brickToAdd);
	}

	public bool CanUseBrick ()
	{
		return bricksCount > 0;
	}

	public void UseBrick ()
	{
		if (CanUseBrick ()) {
			HideTheNextBrickInIventory ();
		}
	}

	private void HideTheNextBrickInIventory ()
	{
		bricksCount--;
		bricks [bricksCount].SetActive (false);
	}
}
