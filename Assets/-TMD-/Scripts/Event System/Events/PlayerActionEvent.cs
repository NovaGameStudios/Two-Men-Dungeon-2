using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerActionEvent : CustomCancellableEvent {
	public readonly PlayerController player;
	public readonly PlayerAction action;

	public PlayerActionEvent (PlayerController player, PlayerAction action) {
		this.player = player;
		this.action = action;
	}
}

