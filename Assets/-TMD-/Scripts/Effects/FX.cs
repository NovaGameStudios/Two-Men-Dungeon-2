using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FX : MonoBehaviour {

//	public PlayerController player;

	public bool isRunning;

	void Start () {
//		player = GetComponentInParent<PlayerController> ();
	}
	void Update () {}

	abstract public void onStart ();
	public void startFX () {
		if (!isRunning) onStart ();
		isRunning = true;
	}
	public void toogleFX () {
		if (!isRunning) onStart ();
		else onStop ();
		isRunning = !isRunning;
	}
	abstract public void onStop ();
	public void stopFX () {
		if (isRunning) onStop ();
		isRunning = false;
	}
}
