using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {

	public float roationSpeed = 180f;
	public float movementSpeed = 4f;

	public Joystick moveJoystick;
	public Joystick lookJoystick;
	public PlayerData playerData;

	private Vector3 movement;
	private Vector3 looking;
	private Vector3 mousePosition;

	private CharacterController characterController;
	private AttackMechanics playerAttack;
	private InventoryController playerInventory;

	private void Awake ()
	{
		characterController = GetComponent<CharacterController> ();
		playerAttack = GetComponent<AttackMechanics> ();
		playerInventory = GetComponent<InventoryController> ();

		if (LevelManager.IsFirstTutorialLevel ()) {
			playerData.bricksInInventory = 0;
		}
		playerInventory.SetInitialBricksInInventory (playerData.bricksInInventory);

		// TODO: get this from player data
		playerAttack.SetThrowForce (playerData.playerForce);
		playerAttack.SetThrowInacuracy (playerData.playerInaccuracy);
		playerAttack.UpdateAttackConeUI ();

	}

	void Update ()
	{
		// MOBILE
		// moving and looking with joystick
		//movement.x = moveJoystick.Horizontal;
		//movement.y = 0;
		//movement.z = moveJoystick.Vertical;

		//looking.x = lookJoystick.Horizontal;
		//looking.y = 0;
		//looking.z = lookJoystick.Vertical;

		//LookWithJoystick ();
		//Move ();


		//DESCKTOP
		//looking with keybaord
		mousePosition = Input.mousePosition;
		LookWithMouse ();
		//LookWithKeyboard ();

		//moving with keyboard
		movement.x = Input.GetAxisRaw ("Horizontal");
		movement.y = 0;
		movement.z = Input.GetAxisRaw ("Vertical");
		Move ();

		//attacking
		if (Input.GetButtonDown ("Fire1")) {
			Shoot ();
		}

	}

	public void Shoot ()
	{
		if (playerInventory.HasBrickAvailable ()) {
			playerAttack.Throw ();
			playerInventory.UseBrick ();
			playerData.bricksInInventory = playerInventory.bricksCount;

			// TODO: play attack sound
		} else {
			// TODO: play no brick sound
		}
	}

	private void Move ()
	{
		// Movement
		Vector3 motion = movement;
		motion -= Vector3.up * 9.1f; // Add gravity
		motion *= (Mathf.Abs (movement.x) == 1 && Mathf.Abs (movement.z) == 1) ? .7f : 1f;

		characterController.Move (motion * movementSpeed * Time.deltaTime);

	}

	//private void LookWithJoystick ()
	//{
	//	// Looking
	//	if (looking != Vector3.zero) {
	//		Quaternion targetRotation = Quaternion.LookRotation (looking);
	//		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, roationSpeed * Time.deltaTime);
	//	}
	//}

	//private void LookWithKeyboard ()
	//{
	//	// Looking
	//	if (movement != Vector3.zero) {
	//		Quaternion targetRotation = Quaternion.LookRotation (movement);
	//		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, roationSpeed * Time.deltaTime);
	//	}
	//}

	private void LookWithMouse ()
	{
		// Looking
		Camera camera = Camera.main;
		mousePosition = camera.ScreenToWorldPoint (new Vector3 (mousePosition.x, mousePosition.y, camera.transform.position.y - transform.position.y));

		Quaternion targetRotation = Quaternion.LookRotation (mousePosition - new Vector3 (transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, roationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Brick" && playerInventory.HasRoomForMoreBricks ()) {
			playerInventory.AddBrick (other.gameObject);
			playerData.bricksInInventory = playerInventory.bricksCount;
		}
	}

}
