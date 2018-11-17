using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : CustomEvent {
	public readonly TriggerManager sender;
	public readonly Collider2D cause;
	public readonly CollisionState state;

	public TriggerEvent (TriggerManager sender, Collider2D cause, CollisionState state) {
		this.sender = sender;
		this.cause = cause;
		this.state = state;
	}
}
