using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject {
	public int bricksInInventory;

	public float playerInaccuracy = 0.25f;  // from 0.25 to 0.05 ( -0.05)
	public float playerForce = 8f;         // from 8 to 14 ( +2)
}
