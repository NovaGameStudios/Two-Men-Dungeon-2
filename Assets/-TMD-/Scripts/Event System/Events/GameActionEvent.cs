using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionEvent : CustomCancellableEvent {
	public readonly GameAction action;

	public GameActionEvent (GameAction action) {
		this.action = action;
	}
}