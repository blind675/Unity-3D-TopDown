using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
	[SerializeField]
	public float maxLife = 100;
	[SerializeField]
	public float armour = 1;
	[SerializeField]
	public float inaccuracy = 0.25f;
	[SerializeField]
	public float attackForce = 8f;
	[SerializeField]
	public float attacksPerSecond = 1f;
	[SerializeField]
	public int bricksStartCount = 5;

	[SerializeField]
	public int trackRange = 7;
	[SerializeField]
	public int chaseRange = 5;
	[SerializeField]
	public int attackRange = 3;
	[SerializeField]
	public float walkSpeed = 3;

}
