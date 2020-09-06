using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyController : MonoBehaviour {
	//FixMe: Just make a class for each button already
	// ... big messy code

	public Button buyAccuracyButton;
	public Button buyPowerButton;
	public Button buyStartBricksButton;
	public Button buyHealthButton;
	public Button buyArmourButton;
	public Button buyInventorySizeButton;

	public Text accuracyPriceText;
	public Text powerPriceText;
	public Text startBricksPriceText;
	public Text healthPriceText;
	public Text armourPriceText;
	public Text inventoryPriceText;

	public Text coinsText;

	public PlayerData playerData;

	public GameObject player;
	public AudioClip buySound;

	private AttackMechanics playerAttackController;
	private InventoryController playerInventory;

	void Start ()
	{
		playerAttackController = player.GetComponent<AttackMechanics> ();
		playerInventory = player.GetComponent<InventoryController> ();

		UpdateCoinsText ();

		UpdateAllButtons ();

		UpdatePlayerAttackCone ();

		UpdatePlayerBricksUI ();
	}

	private void UpdateAllButtons ()
	{
		UpdateCoinsText ();

		UpdateBuyAccuracyButtonUI ();
		UpdateBuyPowerButtonUI ();
		UpdateBuyStartBricksButtonUI ();

		UpdateBuyHealthButtonUI ();
		UpdateBuyArmourButtonUI ();

		UpdateBuyInventorySizeButtonUI ();
	}

	// Buy Accuracy
	public void BuyAccuracy ()
	{
		Action increaseAccuracy = () => playerData.playerInaccuracy -= 0.05f;
		ProcessBuyItem (increaseAccuracy, GetAccuracyPrice);

		UpdateAllButtons ();
		// update the player cone view
		UpdatePlayerAttackCone ();

		AudioSource.PlayClipAtPoint (buySound, gameObject.transform.position);
	}

	private void UpdateBuyAccuracyButtonUI ()
	{
		int accuracyPrice = GetAccuracyPrice ();

		if (Math.Round (1000 * playerData.playerInaccuracy) == Math.Round (1000 * PlayerData.MAX_PLAYER_INACCURACY)) {
			buyAccuracyButton.enabled = false;
			buyAccuracyButton.interactable = false;

			accuracyPriceText.color = new Color (1f, 0.6705883f, 0f);
			accuracyPriceText.text = "Max Level";
		} else {
			accuracyPriceText.text = "- " + accuracyPrice + " Coins";
			if (accuracyPrice > playerData.coins) {
				buyAccuracyButton.enabled = false;
				accuracyPriceText.color = Color.gray;
			} else {
				buyAccuracyButton.enabled = true;
				accuracyPriceText.color = new Color (1f, 0.6705883f, 0f);
			}
		}
	}

	private int GetAccuracyPrice () => PriceForValue (5 - Mathf.RoundToInt ((playerData.playerInaccuracy - 0.05f) / 0.05f));

	// Buy Force
	public void BuyPower ()
	{
		Action increasePower = () => playerData.playerForce += 2;
		ProcessBuyItem (increasePower, GetPowerPrice);

		UpdateAllButtons ();
		// update the player cone view
		UpdatePlayerAttackCone ();

		AudioSource.PlayClipAtPoint (buySound, gameObject.transform.position);
	}

	private void UpdateBuyPowerButtonUI ()
	{
		int powerPrice = GetPowerPrice ();

		if (playerData.playerForce == PlayerData.MAX_PLAYER_FORCE) {
			buyPowerButton.enabled = false;
			buyPowerButton.interactable = false;

			powerPriceText.color = new Color (1f, 0.6705883f, 0f);
			powerPriceText.text = "Max Level";
		} else {
			powerPriceText.text = "- " + powerPrice + " Coins";
			if (powerPrice > playerData.coins) {
				buyPowerButton.enabled = false;
				powerPriceText.color = Color.gray;
			} else {
				buyPowerButton.enabled = true;
				powerPriceText.color = new Color (1f, 0.6705883f, 0f);
			}
		}
	}

	private int GetPowerPrice () => PriceForValue (Mathf.RoundToInt ((playerData.playerForce - 8) / 2) + 1);


	// Buy Start Bricks
	public void BuyStartBricks ()
	{
		// also check the inventory size
		Action addStartBricks = () => playerData.startBricksCount += 1;
		ProcessBuyItem (addStartBricks, GetStartBricksPrice);

		UpdateAllButtons ();
		UpdatePlayerBricksUI ();

		AudioSource.PlayClipAtPoint (buySound, gameObject.transform.position);
	}

	private void UpdateBuyStartBricksButtonUI ()
	{
		int startBricksPrice = GetStartBricksPrice ();

		if (playerData.startBricksCount == playerData.inventorySize) {
			buyStartBricksButton.enabled = false;
			buyStartBricksButton.interactable = false;

			startBricksPriceText.color = new Color (1f, 0.6705883f, 0f);
			startBricksPriceText.text = "Increase Inventory Size";
		} else {
			buyStartBricksButton.interactable = true;

			startBricksPriceText.text = "- " + startBricksPrice + " Coins";
			if (startBricksPrice > playerData.coins) {
				buyStartBricksButton.enabled = false;
				startBricksPriceText.color = Color.gray;
			} else {
				buyStartBricksButton.enabled = true;
				startBricksPriceText.color = new Color (1f, 0.6705883f, 0f);
			}
		}
	}


	private int GetStartBricksPrice () => PriceForValue (playerData.startBricksCount + 1);

	// Buy Health
	public void BuyHealth ()
	{
		Action addHealth = () => playerData.health += 10;
		ProcessBuyItem (addHealth, GetHealthPrice);

		UpdateAllButtons ();

		AudioSource.PlayClipAtPoint (buySound, gameObject.transform.position);
	}

	private void UpdateBuyHealthButtonUI ()
	{
		int healthPrice = GetHealthPrice ();
		healthPriceText.text = "- " + healthPrice + " Coins";

		if (healthPrice > playerData.coins) {
			buyHealthButton.enabled = false;
			healthPriceText.color = Color.gray;
		} else {
			buyHealthButton.enabled = true;
			healthPriceText.color = new Color (1f, 0.6705883f, 0f);
		}
	}

	private int GetHealthPrice () => PriceForValue (Mathf.RoundToInt ((playerData.health - 100) / 10) + 1);


	// Buy Armour
	public void BuyArmour ()
	{
		Action addArmour = () => playerData.armour += 1;
		ProcessBuyItem (addArmour, GetArmourPrice);

		UpdateAllButtons ();

		AudioSource.PlayClipAtPoint (buySound, gameObject.transform.position);
	}

	private int GetArmourPrice () => PriceForValue (playerData.armour + 1);

	private void UpdateBuyArmourButtonUI ()
	{
		int armourPrice = GetArmourPrice ();
		armourPriceText.text = "- " + armourPrice + " Coins";

		if (armourPrice > playerData.coins) {
			buyArmourButton.enabled = false;
			armourPriceText.color = Color.gray;
		} else {
			buyArmourButton.enabled = true;
			armourPriceText.color = new Color (1f, 0.6705883f, 0f);
		}
	}

	// Buy Inventory Size
	public void BuyInventorySize ()
	{
		Action addInventorySize = () => playerData.inventorySize += 1;
		ProcessBuyItem (addInventorySize, GetInventorySizePrice);

		UpdateAllButtons ();

		AudioSource.PlayClipAtPoint (buySound, gameObject.transform.position);
	}

	private void UpdateBuyInventorySizeButtonUI ()
	{
		int inventorySizePrice = GetInventorySizePrice ();

		if (playerData.inventorySize == PlayerData.MAX_INVENTORY_SIZE) {
			buyInventorySizeButton.enabled = false;
			buyInventorySizeButton.interactable = false;

			inventoryPriceText.color = new Color (1f, 0.6705883f, 0f);
			inventoryPriceText.text = "Max Level";
		} else {
			inventoryPriceText.text = "- " + inventorySizePrice + " Coins";
			if (inventorySizePrice > playerData.coins) {
				buyInventorySizeButton.enabled = false;
				inventoryPriceText.color = Color.gray;
			} else {
				buyInventorySizeButton.enabled = true;
				inventoryPriceText.color = new Color (1f, 0.6705883f, 0f);
			}
		}
	}

	private int GetInventorySizePrice () => PriceForValue (playerData.inventorySize);


	private void ProcessBuyItem (Action updateItemFunc, Func<int> priceForItemFunc)
	{
		int itemPrice = priceForItemFunc ();

		if (itemPrice > playerData.coins) {
			return;
		}

		updateItemFunc ();

		playerData.coins -= itemPrice;
	}

	private void UpdateCoinsText ()
	{
		coinsText.text = "Coins: " + playerData.coins;
	}

	private int PriceForValue (int value) => Mathf.RoundToInt ((1 + Mathf.Pow (value - 1, 3) * 4));

	private void UpdatePlayerAttackCone ()
	{
		playerAttackController.SetThrowForce (playerData.playerForce);
		playerAttackController.SetThrowInacuracy (playerData.playerInaccuracy);
		playerAttackController.UpdateAttackConeUI ();
	}

	private void UpdatePlayerBricksUI ()
	{
		playerInventory.SetInitialBricksInInventory (playerData.startBricksCount);
	}
}
