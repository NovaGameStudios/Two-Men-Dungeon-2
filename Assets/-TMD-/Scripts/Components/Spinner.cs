﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
	public float velocity; // Degrees/Second

	void Update () {
		transform.Rotate (Vector3.forward, velocity * Time.deltaTime);
	}
}
