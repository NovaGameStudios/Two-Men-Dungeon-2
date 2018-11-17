using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHit2DEvent : CustomEvent {
	
	public readonly Raycast2D raycast;
	public readonly RaycastHit2D hit;

	public RaycastHit2DEvent (Raycast2D raycast, RaycastHit2D hit) {
		this.raycast = raycast;
		this.hit = hit;
	}
}
