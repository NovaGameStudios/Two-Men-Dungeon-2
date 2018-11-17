using UnityEngine;
using System.Collections;

[System.Serializable]
public class CustomSlideAnimator {

	private RectTransform element;
	public AnimationCurve curve;
	public float velocity;

	public bool onTarget;

	private float startTime;
	private Vector2 startPos;

	private Vector2 target;
	private Vector2 prevTarget;

	public void setRectTransform (RectTransform rectTransform) {
		element = rectTransform;
	}

	public Vector2 getTarget () {
		Debug.Log ("target");
		return target;
	}

	public void setTarget (Vector2 newTarget) {
		target = newTarget;
	}

	public void goToTarget () {
		element.anchoredPosition = target;
	}

	void manageNewTarget (float time) {
		startTime = time;
		startPos = element.anchoredPosition;
		prevTarget = target;
	}

	public void update (float time) {
		if (element.anchoredPosition != target) {
			onTarget = false;
			if (target != prevTarget)
				manageNewTarget (time);

			float maxDist = Vector2.Distance (startPos, target);
			float maxTime = maxDist / velocity;
			float deltaTime = time - startTime;

			if (deltaTime < maxTime) {
				float newDist = curve.Evaluate (deltaTime / maxTime) * maxDist;
				Vector2 newPos = target - startPos;
				newPos.Normalize ();
				newPos *= newDist;
				newPos += startPos;
				element.anchoredPosition = newPos;
			} else goToTarget ();
		} else onTarget = true;
	}
}
