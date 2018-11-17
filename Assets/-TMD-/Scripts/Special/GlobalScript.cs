using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GlobalScript : MonoBehaviour {

	public PhysicsMaterial2D defaultMaterial;
	public LayerMask worldLayerMask;
	public readonly static string globalGameObjectTag = "Global";
//	public readonly static string platformTag = "Platform";
	public static GameObject globalGO;
	private static GlobalScript instance;
	public static GlobalScript Instance {
		get { 
			if (instance == null) instance = getInstance ();
			return instance;
		}
		private set { instance = value; }
	}

	void Start () {
		globalGO = getGlobalGO ();

		foreach (Renderer renderer in GameObject.FindObjectsOfType<Renderer> ()) {
			if (worldLayerMask.containsLayer (renderer.gameObject.layer)) renderer.sortingLayerName = "World";
		}

		foreach (Collider2D collider in GameObject.FindObjectsOfType<Collider2D> ()) {
			if (collider.sharedMaterial != null) continue;
			collider.sharedMaterial = defaultMaterial;
		}
//		foreach (GameObject platform in GameObject.FindGameObjectsWithTag (platformTag)) {
//			if (platform.GetComponent<PlatformEffector2D> () != null || platform.GetComponent<Collider2D> () == null) continue;
//			PlatformEffector2D effector = platform.AddComponent<PlatformEffector2D> ();
//			effector.useColliderMask = false;
//			effector.useOneWay = false;
//			effector.useSideFriction = false;
//			effector.sideArc = 90;
//		}
	}

	private static GameObject getGlobalGO () {
		GameObject globalGO = GameObject.FindWithTag (globalGameObjectTag);
		if (!globalGO) throw new UnityException ("Global GameObject not found");
		return globalGO;
	}

	private static GlobalScript getInstance () {
		GlobalScript gs = getGlobalGO ().GetComponent<GlobalScript> ();
		if (!gs) throw new UnityException ("GlobalScript not found");
		return gs;
	}

//	public void callEvent (CustomEvent cEvent) {
//		if (cEvent is PlayerActionEvent) {
//			Debug.Log ("Event: "+cEvent.targetFunctionName+" -> "+(cEvent as PlayerActionEvent).action);
//		} else Debug.Log ("Event: "+cEvent.targetFunctionName);
//		BroadcastMessage (cEvent.targetFunctionName, cEvent);
//	}
}
