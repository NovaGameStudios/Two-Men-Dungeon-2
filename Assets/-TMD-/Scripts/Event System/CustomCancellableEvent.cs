using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomCancellableEvent : CustomEvent {
	public bool isCancelled;

	public CustomCancellableEvent () {}
}