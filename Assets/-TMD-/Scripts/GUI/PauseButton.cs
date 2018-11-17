using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

	public GameObject pauseButton;
	public GameObject unpauseButton;

	public void gamePause () {
		pauseButton.SetActive (false);
		unpauseButton.SetActive (true);
	}
	public void gameUnpause () {
		pauseButton.SetActive (true);
		unpauseButton.SetActive (false);
	}
}
