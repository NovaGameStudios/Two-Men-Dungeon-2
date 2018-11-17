using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
	public string label;

	private Button button;
	public Button Button {
		get {
			if (!button) button = GetComponent<Button> ();
			return button;
		}
		private set { button = value; }
	}

	void OnClick () {
		new ButtonClickEvent (this).call ();
	}
}