using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameManager : MonoBehaviour {

	public bool isPaused = false;
	public UnityEvent gamePauseEvent;
	public UnityEvent gameUnpauseEvent;
//	public delegate void pauseAction();
//	public static event pauseAction OnGamePause;
//	public static event pauseAction OnGameUnpause;

	public MenuElement inGameMenu;

	// Use this for initialization
	void Start () {
		reset ();
	}

	public void reset () {
		inGameMenu.resetAllChildren ();
		inGameMenu.resetElement ();
	}

	public void pauseTheGame () {
		if (gamePauseEvent != null) gamePauseEvent.Invoke ();
		Time.timeScale = 0;
		isPaused = true;
	}
	public void unpauseTheGame () {
		if (gameUnpauseEvent != null) gameUnpauseEvent.Invoke ();
		Time.timeScale = 1;
		isPaused = false;
	}

    // Update is called once per frame
    void Update () {
		
	}
}
