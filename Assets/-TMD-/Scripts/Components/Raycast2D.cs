using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Raycast2D : MonoBehaviour {

	public string label;
	[Space(10)]
	public bool sendEvent = false;
	[Space(10)]
	public bool useAbsolutePosition = false;
	public Vector2 origin;
	public Vector2 direction;
	[Space(10)]
	public bool useDistance = true;
	[ConditionalField("useDistance")] public float distance = 1;
	public bool useLayerMask = false;
	[ConditionalField("useLayerMask")] public LayerMask layerMask = Physics.DefaultRaycastLayers;
	public bool useMinDepth = false;
	[ConditionalField("useMinDepth")] public float minDepth = 1;
	public bool useMaxDepth = false;
	[ConditionalField("useMaxDepth")] public float maxDepth = 1;
	[Space(10)]
	public bool useDebug = true;
	[ConditionalField("useDebug")] public Color debugColor = Color.cyan;
	[ConditionalField("useDebug")] public float debugDuration = 0.1f;

	public bool hitCollider = false;

	private RaycastHit2D lastHit;
	public RaycastHit2D LastHit {
		get { return lastHit; }
		private set {
			lastHit = value;
			hitCollider = lastHit.collider != null;
		}
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = debugColor;
		Vector2 origin = useAbsolutePosition ? this.origin : (Vector2) (transform.position + transform.rotation * Vector3.Scale (this.origin, transform.lossyScale));
		Vector2 direction = useAbsolutePosition ? this.direction : (Vector2) (transform.rotation * Vector3.Scale (this.direction, transform.lossyScale));
		Gizmos.DrawSphere (origin, 0.1f);
		Gizmos.DrawRay (origin, direction.normalized * distance);
	}

	void Update () {
		if (!sendEvent) return;
		perform ();
		if (hitCollider) new RaycastHit2DEvent (this, LastHit).call ();
	}

	public RaycastHit2D perform () {
		return perform (useLayerMask ? (int) layerMask : (int) Physics.DefaultRaycastLayers);
	}
	public RaycastHit2D perform (LayerMask mask) {
		if (useDebug) Debug.DrawRay (origin, direction.normalized * distance, debugColor, debugDuration);
		LastHit = Physics2D.Raycast (
			useAbsolutePosition ? this.origin : (Vector2) (transform.position + transform.rotation * Vector3.Scale (this.origin, transform.lossyScale)),
			useAbsolutePosition ? this.direction : (Vector2) (transform.rotation * Vector3.Scale (this.direction, transform.lossyScale)),
			useDistance ? distance : Mathf.Infinity,
			mask,
			useMinDepth ? minDepth : Mathf.Infinity,
			useMaxDepth ? maxDepth : Mathf.Infinity);
		Debug.Log ("Raycast hit collider: " + hitCollider);
		return LastHit;
	}

	public static RaycastHit2D perform (Vector2 origin, Vector2 direction, float distance, LayerMask mask) {
		return perform (origin, direction, distance, mask, Color.cyan);
	}
	public static RaycastHit2D perform (Vector2 origin, Vector2 direction, float distance, LayerMask mask, Color color) {
		Debug.DrawRay (origin, direction * distance, color, 0.1f);
		return Physics2D.Raycast (origin, direction, distance, mask);
	}
}
