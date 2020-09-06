﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	[SerializeField]
	private float rotationSpeed = 15f;

	void Update ()
	{
		transform.Rotate (Vector3.up * Time.deltaTime * rotationSpeed);
	}
}