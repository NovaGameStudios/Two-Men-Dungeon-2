using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Util {

	public static bool containsLayer (this LayerMask mask, int layer) {
		return mask.value == (mask | (1<<layer));
	}
	public static ContactFilter2D toContactFilter (this LayerMask mask) {
		ContactFilter2D filter = new ContactFilter2D ();
		filter.SetLayerMask (mask);
		return filter;
	}

	public static GameManager gameManager (this object obj) {
		return GameManager.instance;
	}

	public static float map (float value, float canMin, float canMax, float setMin, float setMax) {
		return Mathf.Lerp (setMin, setMax, Mathf.InverseLerp (canMin, canMax, value));
	}

	public static Raycast2D getRaycast (this MonoBehaviour container, string name) {
		Raycast2D[] components = container.GetComponents<Raycast2D> ();
		foreach (Raycast2D component in components) {
			if (component.label.Equals (name)) return component;
		}
		return null;
	}
	public static Raycast2D getRaycastInChildren (this MonoBehaviour container, string name) {
		Raycast2D[] components = container.GetComponentsInChildren<Raycast2D> ();
		foreach (Raycast2D component in components)
			if (component.label.Equals (name)) return component;
		return null;
	}
	public static Raycast2D[] getRaycasts (this MonoBehaviour container, string name) {
		Raycast2D[] components = container.GetComponents<Raycast2D> ();
		Raycast2D[] raycasts = new Raycast2D[0];
		foreach (Raycast2D component in components) {
			if (component.label.Equals (name)) {
				System.Array.Resize (ref raycasts, (int) raycasts.Length + 1);
				raycasts [raycasts.Length - 1] = component;
			}
		}
		return raycasts;
	}
	public static Raycast2D[] getRaycastsInChildren (this MonoBehaviour container, string name) {
		Raycast2D[] components = container.GetComponentsInChildren<Raycast2D> ();
		Raycast2D[] raycasts = new Raycast2D[0];
		foreach (Raycast2D component in components) {
			if (component.label.Equals (name)) {
				System.Array.Resize (ref raycasts, raycasts.Length + 1);
				raycasts [raycasts.Length - 1] = component;
			}
		}
		return raycasts;
	}
}
