using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour {
	private void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.tag == "Player") {
			SceneController.EndLevel ();
		}
	}
}
