using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent : CustomCancellableEvent {

	public readonly ButtonManager sender;

	public ButtonClickEvent (ButtonManager sender) {
		this.sender = sender;
	}
}