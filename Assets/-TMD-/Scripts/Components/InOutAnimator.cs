using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InOutAnimator {
	
	public float min;
	public float max;
	public ValueAnimator inAnimator;
	public ValueAnimator outAnimator;

	public ValueAnimator currentAnimator {
		get {
			if (state == InOutState.InAnim) return inAnimator;
			if (state == InOutState.OutAnim) return outAnimator;
			return null;
		}
	}

	public bool sleeping {
		get { return state == InOutState.Minimum || state == InOutState.Maximum; }
	}
	[HideInInspector] public float value;

	public enum InOutState { Minimum, Maximum, InAnim, OutAnim}
	public InOutState state = InOutState.Minimum;

//	public delegate void OnFinish ();
//	[HideInInspector] public OnFinish onInFinish;
//	[HideInInspector] public OnFinish onOutFinish;

	public void Init () {
//		inAnimator.onFinish += finishIn;
//		outAnimator.onFinish += finishOut;
		if (state == InOutState.Minimum) value = min;
		if (state == InOutState.Maximum) value = max;
	}

//	private void finishIn () {
//		state = InOutState.Maximum;
//		finished = true;
//		if (onInFinish != null) onInFinish ();
//	}

//	private void finishOut () {
//		state = InOutState.Minimum;
//		finished = true;
//		if (onOutFinish != null) onOutFinish ();
//	}

	public CustomAsyncOperation In () {
//		finished = false;
		CustomAsyncOperation operation = inAnimator.StartAnimation (min, max);
		state = InOutState.InAnim;
		operation.OnComplete += (op) => state = InOutState.Maximum;
		return operation;
	}

	public CustomAsyncOperation Out () {
//		finished = false;
		CustomAsyncOperation operation = outAnimator.StartAnimation (max, min);
		state = InOutState.OutAnim;
		operation.OnComplete += (op) => state = InOutState.Minimum;
		return operation;
	}

	public void Update () {
//		Debug.Log ("Out: " + outAnimator.finished);
		if (sleeping) return;
		ValueAnimator animator = currentAnimator;
		if (animator == null) return;
		animator.Update ();
		value = animator.value;
	}
}