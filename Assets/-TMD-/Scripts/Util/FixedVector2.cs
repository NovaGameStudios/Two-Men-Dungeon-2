using UnityEngine;
using System.Collections;

[System.Serializable]
public class FixedVector2 {

	public bool isFixedX;
	public bool isFixedY;
	public Vector2 vector;

	public Vector2 fixTo (Vector2 fixTo) {
		Vector2 temp = new Vector2 (vector.x, vector.y);
		if (isFixedX) temp.x = fixTo.x;
		if (isFixedY) temp.y = fixTo.y;
		return temp;
	}
}
