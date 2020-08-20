using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBillBoardScript : MonoBehaviour {
	private new Transform camera;

	// Start is called before the first frame update
	void Start ()
	{
		camera = Camera.main.transform;
	}

	// Update is called once per frame
	private void LateUpdate ()
	{
		transform.LookAt (transform.position + camera.forward);
	}

}
