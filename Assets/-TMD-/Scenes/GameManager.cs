using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState { // TODO needed? (give task to SceneMasters)
	Startup,		// -> LoadingScreen
	LoadingScreen,	// -> MainMenu, Level
	MainMenu,		// -> LoadingScreen
//	LevelMenu,
	GameMenu,		// -> Level, LoadingScreen
//	Tutorial,
	Level,			// -> LevelResult, GameMenu
	LevelResults	// -> LoadingScreen
}

public class GameManager : MonoBehaviour {
	public const bool devMode = true;

	public static GameManager instance = null;


	public GameMenuManager gameMenuManager;
//	private GameState gameState; // needed?
	public LoadingScreen loadingScreen;

//	public bool isReady {
//		get {
//			return loadingScreen.State == LoadingScreen.LoadingScreenState.Sleeping; // TODO add more conditions
//		}
//	}

	void Awake () {


		//Check if instance already exists
		if (instance == null) instance = this;
		else if (instance != this) Destroy (gameObject);    

		DontDestroyOnLoad (gameObject);

//		gameMenuManager = GetComponentInChildren<GameMenuManager> ();
		gameMenuManager.gameObject.SetActive (false);
		loadingScreen.gameObject.SetActive (true);


//		SceneManager.activeSceneChanged += OnActiveSceneChange;

//		if (ngsLogo) {
//			ngsLogo.animator.onOutFinish += InitGame;
//		}
//		InitGame ();
		//Call the InitGame function to initialize the first level
	}
	void Start () {
//		gameState = GameState.Startup;
		SceneMaster.currentInstance.StartScene ();
	}
	// TODO if first start -> start in main menu goes to tutorial level (in MainMenuSceneMaster)
		



//	public void LoadScene (string name, LoadSceneMode mode) {
//		StartCoroutine (loadAsyncScene (name, mode));
//	}
	public void LoadSceneWithLoadingScreen (string sceneName) {
//		gameState = GameState.LoadingScreen;
		loadingScreen.LoadScene (sceneName).OnComplete += op => OnSceneLoaded ();
	}


	private void OnSceneLoaded () {
		Debug.Log ("# Scene loaded: " + SceneManager.GetActiveScene ().name);
		Camera currentCamera = SceneMaster.currentInstance.mainCamera;
		foreach (Canvas canvas in GetComponentsInChildren<Canvas> ()) canvas.worldCamera = currentCamera;

//		if ()

		SceneMaster.currentInstance.StartScene ();
	}




	private IEnumerator loadAsyncScene (string name, LoadSceneMode mode) {
		AsyncOperation loadScene = SceneManager.LoadSceneAsync (name, mode);
		while (!loadScene.isDone) yield return null;
		OnSceneLoaded ();
	}
}
