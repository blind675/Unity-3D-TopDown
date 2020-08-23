using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMechanics : MonoBehaviour {

	// FIXME: dunno if ok ... maybe set to null 
	private float throwThrust = -1;
	private float throwDeviation = -1;

	public Transform spawnLocation;
	public GameObject prefabToSpawn;
	public LineRenderer lineRenderer;

	public void SetThrowInacuracy (float inaccuracy)
	{
		throwDeviation = inaccuracy;
	}

	public void SetThrowForce (float force)
	{
		throwThrust = force;
	}

	public void UpdateAttackConeUI ()
	{
		if (throwDeviation == -1 || throwThrust == -1) {
			Debug.LogError (" Please SetThrowInacuracy() and SetThrowForce() first !");
			return;
		}

		// base values:
		//				- length 15
		//				- width 4
		//
		//coneLength            coneWidth          inaccuracy
		//		15					4					0.25
		//		10					x					0.25      x = 4 * cone.length / 15 
		//		15					x					0.10      x = 4 * n / 0.25

		float contLength = throwThrust + 5f;
		float coneWidthAtMaxInaccuracy = 4f * contLength / 15f;
		float coneWidth = coneWidthAtMaxInaccuracy * throwDeviation / 0.25f;

		lineRenderer.SetPosition (1, new Vector3 (0, 0, contLength));
		lineRenderer.endWidth = coneWidth;

	}

	public void Throw ()
	{
		if (throwDeviation == -1 || throwThrust == -1) {
			Debug.LogError (" Please SetThrowInacuracy() and SetThrowForce() first !");
			return;
		}

		float inaccuracyValue = Random.Range (-throwDeviation, throwDeviation);
		Vector3 directionInacuracy = new Vector3 (inaccuracyValue, 0, inaccuracyValue);
		Vector3 throwDirection = (spawnLocation.forward + directionInacuracy);

		GameObject prefab = Instantiate (prefabToSpawn, spawnLocation.position, Quaternion.LookRotation (throwDirection));
		Vector3 throwForce = prefab.transform.forward * throwThrust;

		prefab.GetComponent<Rigidbody> ().AddForce (throwForce, ForceMode.Impulse);





		////TODO: Add torque on all directions random amount
		////Vector3 directionsComposit = 
		////prefab.GetComponent<Rigidbody> ().AddTorque (prefab.transform.up * 10f);

		//prefab.GetComponent<Rigidbody> ().AddForce (throwForce, ForceMode.Impulse);

		//Ray ray = new Ray (spawnLocation.position, throwForce);
		//RaycastHit hit;

		//if (Physics.Raycast (ray, out hit, maxShootDistance)) {
		//	Debug.DrawRay (ray.origin, ray.direction * hit.distance, Color.red, 1);
		//}
	}

}
