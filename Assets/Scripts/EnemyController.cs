using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EnemyStats))]
[RequireComponent (typeof (AttackMechanics))]
[RequireComponent (typeof (InventoryController))]
public class EnemyController : MonoBehaviour {

	private float lastAttackTime = 0;

	private GameObject player;
	private InventoryController enemyInventory;
	private EnemyStats enemyStats;
	private AttackMechanics attack;

	private void Start ()
	{
		player = GameObject.FindGameObjectsWithTag ("Player") [0];

		enemyStats = GetComponent<EnemyStats> ();
		enemyInventory = GetComponent<InventoryController> ();
		attack = GetComponent<AttackMechanics> ();

		enemyInventory.SetInitialBricksInInventory (enemyStats.bricksStartCount);

		attack.SetThrowInacuracy (enemyStats.inaccuracy);
		attack.SetThrowForce (enemyStats.attackForce);
		attack.UpdateAttackConeUI ();
	}

	// Update is called once per frame
	void Update ()
	{
		float distanceToPlayer = GetDistanceToPlayer ();

		if (distanceToPlayer < enemyStats.trackRange) {
			LookAtPlayer ();
		}

		// TODO: diferent for zombies
		if (enemyInventory.HasBrickAvailable ()) {

			if (distanceToPlayer < enemyStats.chaseRange && distanceToPlayer > 1.5) {

				ChasePlayer ();
			}

			if (distanceToPlayer < enemyStats.attackRange && CanAttackPlayer ()) {
				AttackPlayer ();
			}
		}


		if (BricksOnGroundController.AreBricksOnTheGround () && enemyInventory.HasRoomForMoreBricks ()) {
			// Go Find Bricks

			FindBrick ();
		}
	}

	private float GetDistanceToPlayer () => Vector3.Distance (transform.position, player.transform.position);

	private bool CanAttackPlayer () => Time.time > (1 / enemyStats.attacksPerSecond) + lastAttackTime;

	private void LookAtPlayer ()
	{
		transform.LookAt (player.transform.position);
	}

	private void ChasePlayer ()
	{
		// TODO: use nav mesh
		transform.position = Vector3.MoveTowards (transform.position, player.gameObject.transform.position, enemyStats.walkSpeed * Time.deltaTime);
	}

	private void AttackPlayer ()
	{
		attack.Throw ();
		enemyInventory.UseBrick ();
		lastAttackTime = Time.time;
	}

	private void FindBrick ()
	{
		Transform targetBrickLocation = BricksOnGroundController.GetClosestBrick (transform);

		if (targetBrickLocation == null) return;

		// TODO: use nav mesh
		transform.position = Vector3.MoveTowards (transform.position, targetBrickLocation.position, enemyStats.walkSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Brick" && enemyInventory.HasRoomForMoreBricks ()) {
			enemyInventory.AddBrick (other.gameObject);
			BricksOnGroundController.RemoveBrick (other.gameObject.transform);
		}
	}
}
