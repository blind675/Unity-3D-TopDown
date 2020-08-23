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
		enemyStats = GetComponent<EnemyStats> ();
		enemyInventory = GetComponent<InventoryController> ();
		attack = GetComponent<AttackMechanics> ();

		enemyInventory.SetInitialBricksInInventory (enemyStats.bricksStartCount);

		attack.SetThrowInacuracy (enemyStats.inaccuracy);
		attack.SetThrowForce (enemyStats.attackForce);
		attack.UpdateAttackConeUI ();

		player = GameObject.FindGameObjectsWithTag ("Player") [0];

	}

	// Update is called once per frame
	void Update ()
	{
		if (enemyInventory.HasBrickAvailable ()) {
			float distanceToPlayer = GetDistanceToPlayer ();

			if (distanceToPlayer < enemyStats.attackRange && CanAttackPlayer ()) {
				AttackPlayer ();
			}

			if (distanceToPlayer < enemyStats.chaseRange) {
				ChasePlayer ();
			}

			if (distanceToPlayer < enemyStats.trackRange) {
				LookAtPlayer ();
			}
		} else {
			// Go Find Bricks
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
		// do later
	}

	private void AttackPlayer ()
	{
		attack.Throw ();
		enemyInventory.UseBrick ();
		lastAttackTime = Time.time;
	}
}
