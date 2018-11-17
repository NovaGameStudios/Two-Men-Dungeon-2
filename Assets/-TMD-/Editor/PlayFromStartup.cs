using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFromStartup {
	public const string startupScene = "Startup";

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void BeforeSceneLoad () {
		SceneManager.LoadScene (startupScene, LoadSceneMode.Single);
	}

}