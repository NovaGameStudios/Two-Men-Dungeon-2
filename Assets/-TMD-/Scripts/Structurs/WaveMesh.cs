using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMesh : MonoBehaviour {

	public Wave[] waves;

	private Mesh mesh;
	private Vector3 size;
	private Vector3[] vertices;
	private Vector3[] newVertices;

	void Start () {
		mesh = GetComponent<MeshFilter> ().mesh;
		size = mesh.bounds.size;
		vertices = mesh.vertices;
		newVertices = new Vector3[vertices.Length];
	}

	void Update () {
		for (int i = 0; i < newVertices.Length; i++) {
			Vector3 vertex = vertices[i];
			foreach (Wave w in waves) vertex.z += Mathf.Sin(Time.time * w.speed + vertex.x * 2*Mathf.PI*w.frequency / size.x) * w.scale;
			newVertices[i] = vertex;
		}
		mesh.vertices = newVertices;
		mesh.RecalculateNormals();
	}
}

[Serializable]
public class Wave {
	
	public float scale = 1;
	public float speed = 1;
	public float frequency = 1;

}