using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour {

	public GameObject exitMessageCanvas;

	private void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.tag == "Player") {

			EnemyController [] enemyes = GameObject.FindObjectsOfType<EnemyController> ();

			if (enemyes.Length == 0) {
				exitMessageCanvas.SetActive (false);
				SceneController.EndLevel ();
			} else {
				exitMessageCanvas.SetActive (true);
			}

		}
	}

	private void OnTriggerExit (Collider collider)
	{
		if (collider.gameObject.tag == "Player") {
			exitMessageCanvas.SetActive (false);
		}
	}
}

