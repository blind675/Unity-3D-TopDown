using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplosionFX : MonoBehaviour {

	public float cubeSize = 0.15f;
	public int cubesInRow = 4;
	public float explosionForce = 15f;
	public float explosionRadius = 2f;
	public float explosionUpward = 0.1f;
	public float pieceLifeTime = 0.7f;

	public Material material;

	private float cubesPivotDistance;

	private Vector3 cubesPivot;

	// Use this for initialization
	void Start ()
	{
		//calculate pivot distance
		cubesPivotDistance = cubeSize * cubesInRow / 2;
		//use this value to create pivot vector)
		cubesPivot = new Vector3 (cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

	}

	public void Explode ()
	{
		//make object disappear
		gameObject.SetActive (false);

		//loop 3 times to create 5x5x5 pieces in x,y,z coordinates
		for (int x = 0; x < cubesInRow; x++) {
			for (int y = 0; y < cubesInRow; y++) {
				for (int z = 0; z < cubesInRow; z++) {
					CreatePiece (x, y, z);
				}
			}
		}

		//get explosion position
		Vector3 explosionPos = transform.position;
		//get colliders in that position and radius
		Collider [] colliders = Physics.OverlapSphere (explosionPos, explosionRadius);
		//add explosion force to all colliders in that overlap sphere
		foreach (Collider hit in colliders) {
			//get rigidbody from collider object
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			if (rb != null) {
				//add explosion force to this body with given parameters
				rb.AddExplosionForce (explosionForce, transform.position, explosionRadius, explosionUpward);
			}
		}

		Destroy (gameObject, pieceLifeTime + 0.5f);

	}

	void CreatePiece (int x, int y, int z)
	{
		//create piece
		GameObject piece;
		piece = GameObject.CreatePrimitive (PrimitiveType.Cube);

		//set piece position and scale
		piece.transform.position = transform.position + new Vector3 (cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
		piece.transform.localScale = new Vector3 (cubeSize, cubeSize, cubeSize);

		//add rigidbody and set mass
		piece.AddComponent<Rigidbody> ();
		piece.GetComponent<Rigidbody> ().mass = cubeSize;

		//add custom material
		piece.GetComponent<Renderer> ().material = material;

		float lifeTime = Random.Range (pieceLifeTime - 0.5f, pieceLifeTime + 0.5f);

		Destroy (piece, lifeTime);
	}

}