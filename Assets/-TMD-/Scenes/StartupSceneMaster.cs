using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupSceneMaster : SceneMaster {

	public AlphaFader logo;

	public override void StartScene () {
		StartCoroutine (doStuff ());
	}

	private IEnumerator doStuff () {
		#if UNITY_EDITOR
		if (GameManager.devMode) yield return null;
		else {
		#endif
			yield return new WaitForSecondsRealtime (0.5f);
			yield return logo.fadeIn ();
			yield return new WaitForSecondsRealtime (0.5f);
			yield return logo.fadeOut ();
		#if UNITY_EDITOR
		}
		#endif
		this.gameManager ().LoadSceneWithLoadingScreen ("Main Menu");
	}
}
