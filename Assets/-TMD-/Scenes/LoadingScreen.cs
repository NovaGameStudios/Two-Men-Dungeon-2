using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	public InOutAnimator heightAnimator;
	public float minLoadingTime;

	public enum LoadingScreenState {
		Sleeping, Beginning, Loading, Ending
	}
	private LoadingScreenState state = LoadingScreenState.Sleeping;
	public LoadingScreenState State { get { return state; } }
	public float progress {
		get {
			return state == LoadingScreenState.Sleeping ? 1/4
					: state == LoadingScreenState.Beginning ? 2/4
					: state == LoadingScreenState.Loading ? 3/4
					: state == LoadingScreenState.Ending ? 4/4
					: 0;
		}
	}
//	private string sceneName;
//	private Scene scene;

//	public delegate void OnComplete (Scene newScene);
//	private OnComplete callback;

	void Awake () {
		heightAnimator.Init ();
//		heightAnimator.onInFinish += load;
//		SceneManager.sceneLoaded += end;
//		heightAnimator.onOutFinish += sleep;
	}


	public CustomAsyncOperation LoadScene (string name) {
		if (state != LoadingScreenState.Sleeping) return null;
		#if UNITY_EDITOR
		if (GameManager.devMode) SceneManager.LoadSceneAsync (name);
		else {
		#endif
			state = LoadingScreenState.Beginning;
			StartCoroutine (coroutineFunction (name));
		#if UNITY_EDITOR
		}
		#endif

		return new CustomAsyncOperation (() => state == LoadingScreenState.Sleeping, () => progress, op => {});
	}

	private IEnumerator coroutineFunction (string sceneName) {
		yield return heightAnimator.In ();

		state = LoadingScreenState.Loading;
		CustomTimer minLoadingTimer = new CustomTimer (minLoadingTime);
		yield return SceneManager.LoadSceneAsync (sceneName);
		yield return minLoadingTimer;

		state = LoadingScreenState.Ending;
		yield return heightAnimator.Out ();
		state = LoadingScreenState.Sleeping;
	}

//	public void LoadScene (string name, OnComplete callback) {
//		if (state != LoadingScreenState.Sleeping) return;
//		this.sceneName = name;
//		this.callback = callback;
//		begin ();
//	}
//
//	private void begin () {
//		if (state != LoadingScreenState.Sleeping) return;
//		state = LoadingScreenState.Beginning;
//		heightAnimator.In ();
//	}
//	private void load () {
//		if (state != LoadingScreenState.Beginning) return;
//		state = LoadingScreenState.Loading;
//		GameManager.instance.LoadScene (sceneName, LoadSceneMode.Single);
//	}
//	private void end (Scene scene, LoadSceneMode mode) {
//		if (state != LoadingScreenState.Loading) return;
//		state = LoadingScreenState.Ending;
//		this.scene = scene;
//		heightAnimator.Out ();
//	}
//	private void sleep () {
//		if (state != LoadingScreenState.Ending) return;
//		state = LoadingScreenState.Sleeping;
//		callback (scene);
//	}

	void Update () {
		if (!heightAnimator.sleeping) {
			heightAnimator.Update ();
			transform.localPosition = new Vector3 (transform.localPosition.x, heightAnimator.value, transform.localPosition.z);
		}
	}
}
