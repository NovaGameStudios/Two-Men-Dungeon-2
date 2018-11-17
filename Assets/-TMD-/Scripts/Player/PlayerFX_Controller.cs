using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX_Controller : MonoBehaviour {

	public PlayerController player;
	private bool wasGround;

//	public FX jumpPadStayFX;
//	public FX jumpPadJumpFX;
//	private bool onJumpPad;

	public AudioFX footsteps;
	public AudioFX jump;
	public AudioFX land;

	void Start () {
		if (!player) player = GetComponentInParent<PlayerController> ();
	}

	void onPlayerMove(PlayerActionEvent cEvent) {
		if (cEvent.isCancelled) return;
		if (cEvent.action == PlayerAction.Jump) jump.play ();
	}

	void Update () {
//		if (player.onJumpPad) jumpPadStayFX.startFX (); else jumpPadStayFX.stopFX ();
//		if (!player.onGround && onJumpPad) jumpPadJumpFX.startFX (); else if (!wasGround) jumpPadJumpFX.stopFX ();
		if (!wasGround && player.onGround) land.play ();
		if (player.onGround && !player.atWall && player.isMovingByInput && !player.isSliding && !player.isDead && !land.isRunning) footsteps.startFX ();
		else footsteps.stopFX ();
		wasGround = player.onGround;
//		onJumpPad = player.onJumpPad;
	}
}