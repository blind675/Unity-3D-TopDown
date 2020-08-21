using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	public Texture2D map;
	public ColorToPrefab [] colorMappings;

	void Start ()
	{
		GenerateLevel ();
	}

	// TODO: make versatile 3D and 2D
	void GenerateLevel ()
	{
		for (int x = 0; x < map.width; x++) {
			for (int y = 0; y < map.height; y++) {
				GenerateTile (x, y);
			}
		}
	}

	void GenerateTile (int x, int y)
	{
		Color pixelColor = map.GetPixel (x, y);

		if (pixelColor.a == 0) {
			return;
		}

		//Debug.Log ("Found Color:" + pixelColor.ToString());

		foreach (ColorToPrefab colorMapping in colorMappings) {

			if (colorMapping.color.Equals (pixelColor)) {

				Vector3 position = GetTilePositionFromCoordonates (x, y);

				GameObject prefab = colorMapping.prefab;

				if (colorMapping.setupType == ColorToPrefab.PrefabSetupType.Spawn) {
					prefab = SpawnPrefabAtPosition (colorMapping.prefab, position);
				} else {
					MovePrefabToPosition (colorMapping.prefab, position);
				}

				if (colorMapping.linkedToPlayerGameObject) {
					EnemyController enemyController = prefab.GetComponent<EnemyController> ();

					if (enemyController != null) {
						enemyController.player = GameObject.Find ("Player");
					}
				}

				// TODO: remove this
				//if (colorMapping.cameraFollowPrefab) {
				//	Camera.main.GetComponent<CameraController> ().player = prefab.transform;
				//}
			}
		}
	}

	// photo origin is top left, map origin is center -- corect for that
	private Vector3 GetTilePositionFromCoordonates (int x, int y) => new Vector3 (x - map.width / 2, 0, y - map.height / 2);

	private GameObject SpawnPrefabAtPosition (GameObject prefab, Vector3 position) => Instantiate (prefab, position, Quaternion.identity, transform);

	private void MovePrefabToPosition (GameObject prefab, Vector3 position) => prefab.transform.position = position;
}
