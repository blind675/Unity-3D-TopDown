using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject {
	public const float MAX_PLAYER_INACCURACY = 0.05f;
	public const float MAX_PLAYER_FORCE = 14;
	public const float MAX_INVENTORY_SIZE = 5;
	public const float MAX_START_BRICKS = 5;

	public int coins = 0;

	public int bricksInInventory = 0;

	public int startBricksCount = 0;
	public int inventorySize = 1;

	public int armour = 0;
	public int health = 100;

	public float playerInaccuracy = 0.25f;  // from 0.25 to 0.05 ( -0.05)
	public float playerForce = 8f;         // from 8 to 14 ( +2)
}
