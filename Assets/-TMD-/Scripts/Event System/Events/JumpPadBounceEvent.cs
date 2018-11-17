using UnityEngine;
using System.Collections;

public class JumpPadBounceEvent : CustomCancellableEvent {
	public readonly JumpPad jumpPad;
	public readonly Rigidbody2D rigidbody;

	public JumpPadBounceEvent (JumpPad jumpPad, Rigidbody2D rigidbody) {
		this.jumpPad = jumpPad;
		this.rigidbody = rigidbody;
	}
}

