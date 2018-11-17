using UnityEngine;
using System.Collections;

[System.Serializable]
public class CustomSlideValue {

	private float currentV;
	public AnimationCurve curve;
	public float velocity;

	public bool onTargetV;

	private float startT;
	private float startV;

	private float targetV;
	private float prevTargetVal;

	public void setCurrentVal (float value_) {
		currentV = value_;
	}

	public float getTargetVal () {
		return targetV;
	}

	public void setTargetVal (float newTargetV) {
		targetV = newTargetV;
	}

	public void goToTargetVal () {
		currentV = targetV;
	}

	void manageNewTarget (float time) {
		startT = time;
		startV = currentV;
		prevTargetVal = targetV;
	}

	public float update (float time) {
		if (currentV != targetV) {
			onTargetV = false;

			if (targetV != prevTargetVal) manageNewTarget (time);

			float deltaV = targetV - startV;
			float deltaT = Mathf.Abs (deltaV / velocity);
			float currentT = time - startT;

			if (currentT < deltaT) {
				currentV = Util.map (curve.Evaluate (currentT / deltaT), 0, 1, startV, targetV);
			} else {
				currentV = targetV;
			}
		} else {
			onTargetV = true;
		}
		return currentV;
	}
}
