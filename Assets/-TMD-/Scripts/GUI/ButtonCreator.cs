using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour {
	public ButtonManager prefab;

	public float angleRange;
	public Vector2 velocity;

	void Start () {
		createButton ("Continue", "Continue", new Vector2 (1, 55));
		createButton ("Restart", "Restart Level", new Vector2 (0, 45));
		createButton ("Settings", "Settings", new Vector2 (-1, 35));
		createButton ("Exit", "Exit To Menu", new Vector2 (0, 25));
	}

	public ButtonManager createButton (string name, string text, Vector2 position) {
		ButtonManager button = Instantiate (prefab, position, Quaternion.AngleAxis ((float) (2*(Random.value-0.5)*angleRange), Vector3.forward), transform);
		button.GetComponent<Rigidbody2D> ().velocity = velocity;
		button.name = name;
		button.GetComponentInChildren<Text> ().text = text;
		return button;
	}
}
