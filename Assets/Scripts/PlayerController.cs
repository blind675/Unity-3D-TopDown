using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {

	public float roationSpeed = 450f;
	public float movementSpeed = 5f;

	//public Joystick joystick;

	private Vector3 movement;
	private Vector3 mousePosition;

	private CharacterController characterController;
	private AttackMechanics playerAttack;
	private InventoryController playerInventory;

	private void Start ()
	{
		characterController = GetComponent<CharacterController> ();
		playerAttack = GetComponent<AttackMechanics> ();
		playerInventory = GetComponent<InventoryController> ();
	}

	void Update ()
	{
		// looking 
		mousePosition = Input.mousePosition;
		LookWithMouse ();


		// moving
		movement.x = Input.GetAxisRaw ("Horizontal");
		movement.y = 0;
		movement.z = Input.GetAxisRaw ("Vertical");
		Move ();

		// attacking
		if (Input.GetButtonDown ("Fire1") && playerInventory.CanUseBrick ()) {
			playerAttack.Throw ();
			playerInventory.UseBrick ();
		}

	}

	void Move ()
	{
		// Movement
		Vector3 motion = movement;
		motion -= Vector3.up * 9.1f; // Add gravity
		motion *= (Mathf.Abs (movement.x) == 1 && Mathf.Abs (movement.y) == 1) ? .7f : 1f;

		characterController.Move (motion * movementSpeed * Time.deltaTime);
	}

	void LookWithKeyboard ()
	{
		// Looking
		if (movement != Vector3.zero) {
			Quaternion targetRotation = Quaternion.LookRotation (movement);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, roationSpeed * Time.deltaTime);
		}

	}

	void LookWithMouse ()
	{
		// Looking
		Camera camera = Camera.main;
		mousePosition = camera.ScreenToWorldPoint (new Vector3 (mousePosition.x, mousePosition.y, camera.transform.position.y - transform.position.y));

		Quaternion targetRotation = Quaternion.LookRotation (mousePosition - new Vector3 (transform.position.x, 0, transform.position.z));
		// add the correction +90 , why is that ?
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y + 90, roationSpeed * Time.deltaTime);
	}
}
