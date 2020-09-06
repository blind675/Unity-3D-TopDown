using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	private BoxCollider boxCollider;
	private new Rigidbody rigidbody;

	private void Start ()
	{
		boxCollider = GetComponent<BoxCollider> ();
		rigidbody = GetComponent<Rigidbody> ();
	}

	private void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Ground") {
			boxCollider.isTrigger = true;
			rigidbody.isKinematic = true;
		}
	}
}
