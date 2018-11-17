using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class FunctionCaller : MonoBehaviour {

	public MonoBehaviour targetScript;
	public string targetFunction;
	public float delay;

	void Start () {
		MethodInfo method = targetScript.GetType ().GetMethod (targetFunction);
		if (method == null) Debug.LogError ("Function not found!");
		StartCoroutine (Execute (method));
	}

	IEnumerator Execute (MethodInfo method) {
		yield return new WaitForSecondsRealtime (delay);
		method.Invoke (targetScript, new object[] {});
		Debug.Log ("Executed " + method.Name);
		Destroy (this);
	}
}