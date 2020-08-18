using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMechanics : MonoBehaviour {

	// TODO: calculate this from player stats
	private float throwThrust = 40.0f;
	private float throwDeviation = 0.25f;

	public Transform spawnLocation;
	public GameObject prefabToSpawn;

	public void Throw ()
	{
		float inacuracyValue = Random.Range (-throwDeviation, throwDeviation);
		Vector3 inacuracyForce = new Vector3 (inacuracyValue, 0, inacuracyValue);
		Vector3 throwForce = (spawnLocation.forward + inacuracyForce) * throwThrust;

		GameObject prefab = Instantiate (prefabToSpawn, spawnLocation.position, Quaternion.LookRotation (spawnLocation.forward));

		//TODO: Add torque on all directions random amount
		//Vector3 directionsComposit = 
		//prefab.GetComponent<Rigidbody> ().AddTorque (prefab.transform.up * 10f);


		prefab.GetComponent<Rigidbody> ().AddForce (throwForce, ForceMode.Impulse);

		//Ray ray = new Ray (spawnLocation.position, throwForce);
		//RaycastHit hit;

		//if (Physics.Raycast (ray, out hit, maxShootDistance)) {
		//	Debug.DrawRay (ray.origin, ray.direction * hit.distance, Color.red, 1);
		//}
	}

}
