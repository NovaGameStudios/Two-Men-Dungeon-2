using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour {

	public string label;

	public bool useLayerMask = false;
	[ConditionalField("useLayerMask")] public LayerMask layerMask = Physics.DefaultRaycastLayers;

	private Collider2D trigger;
	public Collider2D Trigger {
		get {
			if (trigger) return trigger;
			Collider2D[] colliders = GetComponents<Collider2D> ();
			foreach (Collider2D collider in colliders) {
				if (!collider.isTrigger) continue;
				trigger = collider;
				return trigger;
			}
			return null;
		}
		private set { trigger = value; }
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (useLayerMask && !layerMask.containsLayer (other.gameObject.layer)) return;
		new TriggerEvent (this, other, CollisionState.Enter).call ();
	}
	void OnTriggerStay2D (Collider2D other) {
		if (useLayerMask && !layerMask.containsLayer (other.gameObject.layer)) return;
		new TriggerEvent (this, other, CollisionState.Stay).call ();
	}
	void OnTriggerExit2D (Collider2D other) {
		if (useLayerMask && !layerMask.containsLayer (other.gameObject.layer)) return;
		new TriggerEvent (this, other, CollisionState.Exit).call ();
	}
}
