using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerBehaviour", order = 1)]
public class PlayerBehaviour : ScriptableObject {

	public float runningMaxVelocity;
	public float runningForce;
	public float jumpImpuls;
	public Vector2 wallJumpImpuls;
	public float slidingImpuls;
	public float airForceUp;
	public float airForceDown;
	public float airForceHorizontal;
	[Range(0, 1)] public float airDragHorizontal;

	[Range(0, 90)] public float maxSlope;
	public float controlMercyTime;
	public int crushMaxTime;

}