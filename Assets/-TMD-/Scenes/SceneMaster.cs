using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneMaster : MonoBehaviour {

	public static SceneMaster currentInstance;

	public SceneInfo info;
	public Camera mainCamera;
	public bool isReady = true;

	void Awake () {
		currentInstance = this;
	}

	public abstract void StartScene ();
}