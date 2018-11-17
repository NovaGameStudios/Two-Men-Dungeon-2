using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : FX {

	public AudioSource audioSource;
	public bool paused;

	// Use this for initialization
	void Start () {
		if (!audioSource) audioSource = GetComponent<AudioSource> ();
	}

	public override void onStart () {
		play ();
	}
	public override void onStop () {
		stop ();
	}

	public void play () {
		audioSource.Play ();
	}
	public void pause () {
		if (!paused) audioSource.Pause ();
		else audioSource.UnPause ();
		paused = !paused;
	}
	public void stop () {
		audioSource.Stop ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
