using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

	public int bricksCount = 0;

	public bool CanPickUpMoreBricks => bricksCount < bricks.Length;
	public bool CanUseBrick => bricksCount > 0;

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


	public void AddBrick (GameObject brickToAdd)
	{
		if (CanPickUpMoreBricks) {
			ActivateTheNextBrickInIventory ();
			DestroyTheBrickFromTheGround (brickToAdd);
		}
	}

	private void ActivateTheNextBrickInIventory ()
	{
		bricks [bricksCount].SetActive (true);
		bricksCount++;
	}

	private void DestroyTheBrickFromTheGround (GameObject brickToAdd)
	{
		Destroy (brickToAdd);
	}

	public void UseBrick ()
	{
		if (CanUseBrick) {
			HideTheNextBrickInIventory ();
		}
	}

	private void HideTheNextBrickInIventory ()
	{
		bricksCount--;
		bricks [bricksCount].SetActive (false);
	}
}
