using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

	public int bricksCount { get; private set; } = 0;

	public PlayerData playerData;
	public GameObject [] bricks;

	public void SetInitialBricksInInventory (int intialBrickCount)
	{
		bricksCount = intialBrickCount;

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
		if (HasRoomForMoreBricks ()) {
			ActivateTheNextBrickInIventory ();
			DestroyTheBrickFromTheGround (brickToAdd);
		}
	}

	public bool HasRoomForMoreBricks () => bricksCount < bricks.Length && bricksCount < playerData.inventorySize;

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
		if (HasBrickAvailable ()) {
			HideTheNextBrickInIventory ();
		}
	}

	public bool HasBrickAvailable () => bricksCount > 0;

	private void HideTheNextBrickInIventory ()
	{
		bricksCount--;
		bricks [bricksCount].SetActive (false);
	}
}
