using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text bricksInventoryCount;
	public PlayerData playerData;

	// Update is called once per frame
	void Update ()
	{
		bricksInventoryCount.text = playerData.bricksInInventory + ": Bricks";
	}
}
