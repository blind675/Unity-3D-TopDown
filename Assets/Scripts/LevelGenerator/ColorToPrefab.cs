using UnityEngine;

[System.Serializable]
public class ColorToPrefab {
	public enum PrefabSetupType {
		Spawn,
		Move
	}

	public Color color;
	public PrefabSetupType setupType = PrefabSetupType.Spawn;
	public GameObject prefab;

	public bool linkedToPlayerGameObject;

	// TODO: remove this
	//public bool cameraFollowPrefab;
}
