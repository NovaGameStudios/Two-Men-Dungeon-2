using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAsyncOperation : CustomYieldInstruction {

	public delegate bool Condition ();
	private Condition isDone;
	public bool IsDone {
		get { return !IsCancelled && isDone != null && isDone (); }
	}
	public bool IsComplete {
		get { return IsDone || IsCancelled; }
	}
	public override bool keepWaiting {
		get { return !IsComplete; }
	}

	public delegate float ProgressFunc ();
	private ProgressFunc progress;
	public float Progress {
		get { return progress != null ? progress () : 0; }
	}

	public bool IsCancelled = false;

	public delegate void Callback (CustomAsyncOperation operation);
	public Callback OnComplete;


	public CustomAsyncOperation () {}

	public CustomAsyncOperation (Condition isDone, ProgressFunc progress) : this (isDone, progress, op => {}) {}
	public CustomAsyncOperation (Condition isDone, ProgressFunc progress, Callback onComplete) {
		initialise (isDone, progress, onComplete);
	}

	protected void initialise (Condition isDone, ProgressFunc progress, Callback onComplete) {
		this.isDone = isDone;
		this.progress = progress;
		this.OnComplete = onComplete;
		GameManager.instance.StartCoroutine (start ());
	}

	private IEnumerator start () {
		yield return this;
		if (OnComplete != null) OnComplete (this);
	}
}