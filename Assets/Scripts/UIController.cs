using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public Text bricksInventoryCount;

	// Update is called once per frame
	void Update ()
	{
		bricksInventoryCount.text = InventoryController.bricksCount + ": Bricks";
	}
}
