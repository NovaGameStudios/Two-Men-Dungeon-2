using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour, CustomListener {
    
	[Space(10)]
	public PlayerNumber weAreNumber;
	[Space(10)]

	// Movement Behavior
	[Header("Movement Behavior")]
	public PlayerBehaviour behaviour;
	[Space(10)]

//	public float runningMaxVelocity; // 25 // 1000
//	public float runningForce; // 3 // 100
//	public WheelJoint2D feet;
//	private JointMotor2D motor;
//	public int fullSpeedTime;
//	public int fullSpeedTimer;
//	public AnimationCurve runningAcceleration;
//	public float slipperyGround; // 0.5
//	[Space(5)]
//	public float jumpForce; // 20
//	public Vector2 wallJumpForce; // 60
//	[Space(5)]
//	public float airControlForceHorizontal; // 80
//	public float airControlForceUp; // 20
//	public float airControlForceDown; // 50
//	[Range(0, 1)] public float airControlDragHorizontal; // 0.07
//	[Space(5)]
//	public float slidingImpuls;
//	public float forceSliding; // 28
	//	public float slipperySliding; // 0.99
//	[Space(5)]
//	[Range(0, 90)] public float maxSlope; // 45

	// Controls
	[Header("Controls")]
	[Space(5)]
	public Controls controls;
	public KeyCode jump {
		get {
			if (weAreNumber == PlayerNumber.One) return controls.p1Jump;
			else return controls.p2Jump;
		}
	}
	public KeyCode slide {
		get {
			if (weAreNumber == PlayerNumber.One) return controls.p1Slide;
			else return controls.p2Slide;
		}
	}
	public KeyCode left {
		get {
			if (weAreNumber == PlayerNumber.One) return controls.p1Left;
			else return controls.p2Left;
		}
	}
	public KeyCode right {
		get {
			if (weAreNumber == PlayerNumber.One) return controls.p1Right;
			else return controls.p2Right;
		}
	}
//	[Space(5)]
//	public float mercyTime;
	private float jumpMercyTime;
	[Space(10)]

	// Colliders
	[Header("Colliders")]
	[Space(5)]
	private Raycast2D[] frontWallJump;
	private Raycast2D[] rearWallJump;
	[Space(5)]
	public GameObject colliderMoving;
	public GameObject colliderSliding;
	public GameObject colliderDead;
	[Space(5)]
	public LayerMask groundMask;
	public LayerMask slidableMask;
	public LayerMask jumpPadMask;
	public LayerMask wallMask; // SW
	public LayerMask deathMask; // LV
	public LayerMask crushMask; // SW PW LV
	[Space(5)]
	public Collider2D groundTrigger;
	public Collider2D groundSlidingTrigger;
	public Collider2D wallFrontTrigger;
	public Collider2D wallBackTrigger;
//	public Collider2D[] crushTriggers;
	[Space(10)]

	// States
	[Header("States")]
	[Space(5)]
	public bool canMove = true;
	[Space(5)]
	public bool onGround;
	public bool onSlope;
	public bool onJumpPad;
	[Space(5)]
	public bool isFacingLeft = false;
	public bool isFacingRight = true;
	[Space(5)]
	public bool atWall;
	public bool atWallLeft;
	public bool atWallRight;
	[Space(5)]
	public bool isMovingByInput; // if left or right is pressed and player is moving
	public bool isSliding;
	[Space(5)]
	public bool isDead;
	public bool gotCrushed;
	public float maxCrushTime;
	private int timeCrushed;
//	[Space(5)]
//	public Vector2 velocity;
	[Space(10)]

	// Some other things
	[Header("Some other things")]
	[Space(5)]
//	public Transform[] objectsToBeTurned;
	[Space(10)]
	private Rigidbody2D player;
	private Animator anim;

	// Initialization
	void Start () {
//		this.register ();

		this.registerHandler<TriggerEvent> (onTrigger);
		this.registerHandler<RaycastHit2DEvent> (onRaycastHit2D);
		this.registerHandler<JumpPadBounceEvent> (onJumpPadBounce);
//		this.subscribe<TriggerEvent> ();
//		this.subscribe<RaycastHit2DEvent> ();
//		this.subscribe<JumpPadBounceEvent> ();
		player = GetComponentInChildren<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
		foreach (Raycast2D raycast in this.getRaycastsInChildren ("Ground")) {
			raycast.useLayerMask = true;
			raycast.layerMask = slidableMask;
		}
		foreach (Raycast2D raycast in this.getRaycastsInChildren ("Front Wall").Concat (this.getRaycastsInChildren ("Rear Wall"))) {
			raycast.useLayerMask = true;
			raycast.layerMask = wallMask;
		}
		jumpMercyTime = behaviour.controlMercyTime;
		frontWallJump = this.getRaycastsInChildren ("Front Wall");
		rearWallJump = this.getRaycastsInChildren ("Rear Wall");
	}

	public Rigidbody2D getRigidbody() {
		return player;
	}

	public void OnGamePause () {
		canMove = false;
	}
	public void OnGameUnpause () {
		canMove = true;
	}

//	public bool isGrounded () {
//		return getGroundHit ().collider != null;
//	}
//	public RaycastHit2D getGroundHit () {
//		RaycastHit2D middleHit = getMiddleGroundHit (); // check middle
//		if (middleHit.collider) return middleHit;
//		RaycastHit2D frontHit = getFrontGroundHit (); // else front
//		if (frontHit.collider) return frontHit;
//		RaycastHit2D rearHit = getRearGroundHit (); // else rear
//		if (rearHit.collider) return rearHit;
//		return middleHit; // finally just return middle
//	}
//	public RaycastHit2D getMiddleGroundHit () {
//		return Raycast.perform (transform.position, Vector2.down, distanceToGround, groundMask);
//	}
//	public RaycastHit2D getFrontGroundHit () {
//		Vector2 origin = isFacingLeft ? new Vector2 (transform.position.x - frontGround, transform.position.y) : new Vector2 (transform.position.x + frontGround, transform.position.y);
//		return Raycast.perform (origin, Vector2.down, distanceToGround, groundMask, Color.green);
//	}
//	public RaycastHit2D getRearGroundHit () {
//		Vector2 origin = isFacingLeft ? new Vector2 (transform.position.x + rearGround, transform.position.y) : new Vector2 (transform.position.x - rearGround, transform.position.y);
//		return Raycast.perform (origin, Vector2.down, distanceToGround, groundMask, Color.red);
//	}

	// For TriggerScripts
//	void setOnGround (bool val) {onGround = val;}
//	void crushedTriggerEnter (Collider2D other) {if (!other.isTrigger) gotCrushed = true;}
//	void crushedTriggerExit (Collider2D other) {if (!other.isTrigger) gotCrushed = true;}
//	void setAtWall (bool val) {atWall = val;}
//	void setAtWallFront (bool val) {
//		if (isFacingLeft) atWallLeft = val;
//		else atWallRight = val;
//	}
//	void setAtWallBack (bool val) {
//		if (isFacingLeft) atWallRight = val;
//		else atWallLeft = val;
//	}

	public bool touchingJumpableWall () {
		if (!atWall) return false;

		bool frontJumpable = true;
		foreach (Raycast2D raycast in frontWallJump) {
			raycast.perform (wallMask);
			if (raycast.LastHit.collider == null) {
				frontJumpable = false;
				break;
			}
//			if (frontJumpable) continue;
//			Solid solid = raycast.lastHit.collider.GetComponent<Solid> ();
//			frontJumpable = solid && solid.isJumpable;
		}
		if (frontJumpable) return true;

		bool rearJumpable = true;
		foreach (Raycast2D raycast in rearWallJump) {
			raycast.perform (wallMask);
			if (raycast.LastHit.collider == null) {
				rearJumpable = false;
				break;
			}
//			if (rearJumpable) continue;
//			Solid solid = raycast.lastHit.collider.GetComponent<Solid> ();
//			rearJumpable = solid && solid.isJumpable;
		}
		return rearJumpable;
	}

//	public bool touchingJumpable (Collider2D trigger, ContactFilter2D filter) {
//		Collider2D[] colliders = new Collider2D[4]; // smaller array = better performance but more detection mistakes
//		trigger.OverlapCollider (filter, colliders);
//		foreach (Collider2D collider in colliders) {
//			if (!collider) continue;
//			Solid solid = collider.gameObject.GetComponent<Solid> ();
//			if (!solid || solid.isJumpable) return true;
//		}
//		return false;
//	}








	void doJump () {
		PlayerActionEvent newEvent = new PlayerActionEvent (this, PlayerAction.Jump);
		newEvent.call ();
		if (newEvent.isCancelled) return;

		player.velocity = new Vector2 (player.velocity.x, 0);
		player.AddForce (Vector2.up * behaviour.jumpImpuls, ForceMode2D.Impulse);
	}
	void doLeftWallJump () {
		PlayerActionEvent newEvent = new PlayerActionEvent (this, PlayerAction.LeftWallJump);
		newEvent.call ();
		if (newEvent.isCancelled) return;

		player.velocity = Vector2.zero;
		player.AddForce ((Vector2.up+Vector2.right) * behaviour.wallJumpImpuls, ForceMode2D.Impulse);
	}
	void doRightWallJump () {
		PlayerActionEvent newEvent = new PlayerActionEvent (this, PlayerAction.RightWallJump);
		newEvent.call ();
		if (newEvent.isCancelled) return;

		player.velocity = Vector2.zero;
		player.AddForce ((Vector2.up+Vector2.left) * behaviour.wallJumpImpuls, ForceMode2D.Impulse);
	}
	void doSlide (Vector2 boost) {
		PlayerActionEvent newEvent = new PlayerActionEvent (this, PlayerAction.Slide);
		newEvent.call ();
		if (newEvent.isCancelled) return;

		player.AddForce (boost * behaviour.slidingImpuls, ForceMode2D.Impulse); // push player once
		isSliding = true;
	}
	void die () {
		isDead = true;
		isSliding = false;
		canMove = false;
	}

	// #### MOVEMENT ####
	// NO passive stuff!
	void updateMovement () {
		
		// JUMP
		jumpMercyTime += Time.deltaTime;
		if (Input.GetKeyDown (jump)) jumpMercyTime = 0;
		if (Input.GetKeyDown (jump) || (jumpMercyTime < behaviour.controlMercyTime)) { // && Input.GetKey (jump)
			if (onGround && (!onSlope || player.velocity.y == 0)) {
				doJump ();
				jumpMercyTime = behaviour.controlMercyTime;
			} else if (!onGround && !onSlope && atWall && touchingJumpableWall ()) {
				if (atWallLeft) doLeftWallJump ();
				else if (atWallRight) doRightWallJump ();
				jumpMercyTime = behaviour.controlMercyTime;
			}
		}


		// SLIDE
//		bool physicsAllowSliding = !(atWallLeft && player.velocity.x < 0) && !(atWallRight && player.velocity.x > 0) && player.velocity.magnitude > 0.1;
		bool physicsAllowSliding = true;
		// start slide
		if (Input.GetKey (slide) && onGround && !onJumpPad && !onSlope && !isSliding && physicsAllowSliding) doSlide (player.velocity.normalized * behaviour.slidingImpuls);
		// cancel slide
		isSliding = isSliding && physicsAllowSliding && (Input.GetKey (slide) || onSlope);


		// AIR CONTROL
		if (!onGround) {
			if (Input.GetKey (jump) && player.velocity.y > 0) 					player.AddForce (Vector2.up 	*  behaviour.airForceUp);
			if (Input.GetKey (slide)) 											player.AddForce (Vector2.down 	* -behaviour.airForceDown);
			if (Input.GetKey (left) && !Input.GetKey (right) && !atWallLeft) 	player.AddForce (Vector2.left 	*  behaviour.airForceHorizontal);
			if (Input.GetKey (right) && !Input.GetKey (left) && !atWallRight) 	player.AddForce (Vector2.right 	*  behaviour.airForceHorizontal);
		}


		// MOVE HORIZONTAL
		if (onGround && !isSliding) {
			// Left
			if (Input.GetKey (left) && !Input.GetKey (right) && !atWallLeft) {
				if (player.velocity.x > -behaviour.runningMaxVelocity) player.AddForce (Vector2.left * behaviour.runningForce, ForceMode2D.Impulse);
				else player.velocity = Vector2.Lerp (player.velocity, Vector2.left * player.velocity.x, Time.deltaTime * 5.0f);
			}
			// Right
			if (Input.GetKey (right) && !Input.GetKey (left) && !atWallRight) {
				if (player.velocity.x < behaviour.runningMaxVelocity) player.AddForce (Vector2.right * behaviour.runningForce, ForceMode2D.Impulse);
				else player.velocity = Vector2.Lerp (player.velocity, Vector2.right * player.velocity.x, Time.deltaTime * 5.0f);
			}
		}
	}



	// #### EVENT ####
//	[CustomEventHandler()]
	void onJumpPadBounce (JumpPadBounceEvent cEvent) {
		Debug.Log ("####JumpPadBounceEvent");
		if (cEvent.isCancelled) return;
		if (Input.GetKey (slide)) cEvent.isCancelled = true;
	}
//	[CustomEventHandler()]
	void onTrigger (TriggerEvent triggerEvent) {
		Debug.Log ("####TriggerEvent");
		if (triggerEvent.sender.label.Equals ("Crush Trigger") && triggerEvent.state == CollisionState.Stay) gotCrushed = isDead || crushMask.containsLayer (triggerEvent.cause.gameObject.layer);
		if (triggerEvent.sender.label.Equals ("Ground") && triggerEvent.state != CollisionState.Stay) {
			onGround = triggerEvent.sender.Trigger.IsTouchingLayers (groundMask);
			onJumpPad = triggerEvent.sender.Trigger.IsTouchingLayers (jumpPadMask);
//			onSlope = false;
//			if (onGround) {
//				RaycastHit2D groundHit = new RaycastHit2D ();
//				foreach (Raycast2D raycast in isSliding ? groundSlidingSlope : groundSlope) {
//					raycast.perform (slidableMask);
//					groundHit = raycast.lastHit;
//					if (groundHit.collider) break;
//				}
//				onSlope = groundHit.collider && Math.Abs (Vector2.Angle (Vector2.up, groundHit.normal)) > maxSlope;
//			}
//			onGround = onGround
//				|| (triggerEvent.state == (CollisionState.Enter | CollisionState.Stay) && triggerEvent.sender.trigger.IsTouchingLayers (groundMask));
//			onGround = onGround
//				&& !(triggerEvent.state == CollisionState.Exit && !triggerEvent.sender.trigger.IsTouchingLayers (groundMask));

		}
		// TODO Wall Trigger
	}
	[CustomEventHandler()]
	void onRaycastHit2D (RaycastHit2DEvent cEvent) {
		Debug.Log ("####RaycastHit2DEvent");
		if (cEvent.raycast.label.Equals ("Ground"))
			onSlope = onGround && Math.Abs (Vector2.Angle (Vector2.up, cEvent.hit.normal)) > behaviour.maxSlope;
		Debug.Log ("Normal Angle: " + Math.Abs (Vector2.Angle (Vector2.up, cEvent.hit.normal)));
		// TODO can Wall Jump ?
	}



	// #### FIXED UPDATE ####
	void FixedUpdate () {

		// Check Ground
//		onGround = (isSliding ? groundSlidingTrigger : groundTrigger).IsTouchingLayers (groundMask);
//		onSlope = false;
//		if (onGround) {
//			RaycastHit2D groundHit = new RaycastHit2D ();
//			foreach (Raycast2D raycast in isSliding ? groundSlidingSlope : groundSlope) {
//				raycast.perform (slidableMask);
//				groundHit = raycast.lastHit;
//				if (groundHit.collider) break;
//			}
//			onSlope = groundHit.collider && Math.Abs (Vector2.Angle (Vector2.up, groundHit.normal)) > maxSlope;
//		}
//		onJumpPad = groundTrigger.IsTouchingLayers (jumpPadMask);

//		velocity = player.velocity;

		// Check Walls
		if (isFacingLeft) {
			atWallLeft = wallFrontTrigger.IsTouchingLayers (wallMask);
			atWallRight = wallBackTrigger.IsTouchingLayers (wallMask);
		} else if (isFacingRight) {
			atWallRight = wallFrontTrigger.IsTouchingLayers (wallMask);
			atWallLeft = wallBackTrigger.IsTouchingLayers (wallMask);
		}
		atWall = atWallLeft || atWallRight;

		// Check Movement
		isMovingByInput = canMove && (Input.GetKey (left) || Input.GetKey (right)) && Math.Abs (player.velocity.x) > 0.1;
		if (canMove && onSlope) {
			if (player.velocity.y > 0)
				player.velocity = Vector2.zero;
			doSlide (Vector2.zero);
		}

		// Check Death
		// crushing
//		gotCrushed = isDead && gotCrushed;
//		if (!gotCrushed) foreach (Collider2D trigger in crushTriggers) gotCrushed = trigger.IsTouchingLayers (crushMask) || gotCrushed;
		if (gotCrushed && !isDead) timeCrushed++;
		else timeCrushed = 0;
		// ouchy objects
		if (player.IsTouchingLayers (deathMask) || timeCrushed > maxCrushTime) isDead = true;

		// Air Drag
		if (!onGround) player.velocity = new Vector2 (player.velocity.x * (1 - behaviour.airDragHorizontal), player.velocity.y);


		//
		if (canMove && !isDead) {
			updateMovement ();

			// Transform Objects
			if (!isFacingLeft && (
			         (!isSliding && Input.GetKey (left) && !Input.GetKey (right))
			         || ((isSliding || (!Input.GetKey (left) && !Input.GetKey (right))) && player.velocity.x < -0.1))) { // && player.velocity.x < -0.1
				isFacingLeft = true;
				isFacingRight = false;
				transform.localScale = new Vector3 (-1, 1, 1);
			} else if (isFacingLeft && (
			         (!isSliding && Input.GetKey (right) && !Input.GetKey (left))
			         || ((isSliding || (!Input.GetKey (left) && !Input.GetKey (right))) && player.velocity.x > 0.1))) { // && player.velocity.x > 0.1
				isFacingRight = true;
				isFacingLeft = false;
				transform.localScale = new Vector3 (1, 1, 1);
			}
		}


		// COLLIDERS
		if (isDead) {
			// Dead
			colliderMoving.SetActive (false);
			colliderSliding.SetActive (false);
			colliderDead.SetActive (true);
		} else if (isSliding) { //  && onGround
			// Sliding
			colliderMoving.SetActive (false);
			colliderSliding.SetActive (true);
			colliderDead.SetActive (false);
		} else if (player.velocity.magnitude > 0.2 && (!isSliding || !onGround)) {
			// Moving
			colliderMoving.SetActive (true);
			colliderSliding.SetActive (false);
			colliderDead.SetActive (false);
		} else {
			// default
			colliderMoving.SetActive (true);
			colliderSliding.SetActive (false);
			colliderDead.SetActive (false);
		}


		// ANIMATOR
		anim.SetFloat ("xVel", Mathf.Abs (player.velocity.x));
		anim.SetFloat ("yVel", player.velocity.y);
		anim.SetBool ("OnGround", onGround);
		anim.SetBool ("Running", isMovingByInput);
		anim.SetBool ("Dead", isDead);
		anim.SetBool ("AtWall", atWall);
		anim.SetBool ("Sliding", isSliding);
	}
}
