using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueAnimator {

	[HideInInspector]
	public bool finished = true;
//	public delegate void OnFinish ();
//	public OnFinish onFinish;
	public CustomAsyncOperation currentOperation;

	[HideInInspector]
	public float start;
	[HideInInspector]
	public float value;
	[HideInInspector]
	public float target;
	[HideInInspector]
	public float time; // Seconds

	public float duration; // Seconds
	public AnimationCurve valueOverTime; // 1/Second

	public CustomAsyncOperation StartAnimation (float start, float target) {
		this.start = start;
		this.value = start;
		this.target = target;
		this.time = 0;
		this.finished = false;
		this.currentOperation = new CustomAsyncOperation (() => finished, () => time / duration, (operation) => {});
		return currentOperation;
	}

	public void Update () {
		if (finished) return;
		time += Time.deltaTime;
		value = Util.map (valueOverTime.Evaluate (time / duration), 0, 1, start, target);
		if (time >= duration) {
			value = target;
			finished = true;
//			if (onFinish != null) onFinish ();
		}
	}
}
