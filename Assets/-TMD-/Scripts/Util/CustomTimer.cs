using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimer : CustomAsyncOperation {

	public readonly float startTime;
	public readonly float time;

	public CustomTimer (float time) : this (time, op => {}) {}

	public CustomTimer (float time, Callback callback) : base () {
		this.startTime = Time.realtimeSinceStartup;
		this.time = time;

		initialise (() => Time.realtimeSinceStartup >= startTime + time, () => Time.realtimeSinceStartup - startTime / startTime + time, callback);
	}
}