using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksOnGroundController : MonoBehaviour {

	private static List<Transform> _bricksLocations = new List<Transform> ();

	public static void AddBrick (Transform brickLocation)
	{
		_bricksLocations.Add (brickLocation);
	}

	public static void RemoveBrick (Transform brickLocation)
	{
		// Dictionanr gameObject transform
		_bricksLocations.Remove (brickLocation);
	}

	public static bool AreBricksOnTheGround () => _bricksLocations.Count > 0;

	public static Transform GetClosestBrick (Transform transform)
	{
		//Debug.Log (" _bricks.Count: " + _bricks.Count);

		if (!AreBricksOnTheGround ()) return null;

		Transform closestBrickLocation = _bricksLocations [0];

		for (int i = 1; i < _bricksLocations.Count; i++) {

			//FIXME objects in the array are not cleared corectly bug
			if (_bricksLocations [i] == null) {
				continue;
			}

			if (closestBrickLocation == null) {
				closestBrickLocation = _bricksLocations [i];
				continue;
			}

			if (Vector3.Distance (transform.position, closestBrickLocation.position) > Vector3.Distance (transform.position, _bricksLocations [i].transform.position)) {
				closestBrickLocation = _bricksLocations [i];

			}
		}

		return closestBrickLocation;
	}
}
