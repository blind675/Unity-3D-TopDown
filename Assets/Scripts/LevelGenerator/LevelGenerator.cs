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

				// TODO: make versatile 3D and 2D

				// photo origin is top left
				// map origin is center -- corect for that
				// TODO: extaract in metod
				Vector3 position = new Vector3 (x - map.width / 2, 0, y - map.height / 2);
				GameObject pefab = Instantiate (colorMapping.prefab, position, Quaternion.identity, transform);

				if (colorMapping.cameraFollowPrefab) {
					Camera.main.GetComponent<CameraController> ().player = pefab.transform;
				}
			}
		}
	}
}
