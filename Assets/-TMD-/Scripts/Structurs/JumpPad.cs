using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : Solid, CustomListener {

	public Collider2D trigger;
	public float bounciness;
	public List<Rigidbody2D> bouncedRigidbodies = new List<Rigidbody2D> ();


	public ParticleSystem particleFX1;
	public int FXTime;
	public ParticleSystem particleFX2;
	private int timer = 0;
	private AudioSource SFX;
	public float maxLightIntensity;
	public AnimationCurve lightIntensity;
	private Light[] lights;
	private float[] baseIntensities;

	// Use this for initialization
	void Start () {
//		this.register ();
//		this.subscribe<PlayerActionEvent> ();

		SFX = GetComponent<AudioSource> ();
		lights = GetComponentsInChildren<Light> ();
		baseIntensities = new float[lights.Length];
		for (int i = 0; i < lights.Length; i++) baseIntensities [i] = lights [i].intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			for (int i = 0; i < lights.Length; i++) lights [i].intensity = baseIntensities [i] + maxLightIntensity * lightIntensity.Evaluate (1 - (float) timer / (float) FXTime);
			timer--;
		} else if (timer <= 0 && particleFX1.isPlaying) particleFX1.Stop ();
	}

	void onPlayerAction(PlayerActionEvent cEvent) {
		if (cEvent.isCancelled) return;
		if (cEvent.action != PlayerAction.Jump || !cEvent.player.getRigidbody ().IsTouching (trigger)) return;

		cEvent.isCancelled = true;
		bounce (cEvent.player.getRigidbody ());
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.otherCollider != trigger) return;

		Rigidbody2D rigidbody = collision.rigidbody;
		if (!rigidbody || bouncedRigidbodies.Contains (rigidbody)) return;

//		PlayerController player = rigidbody.GetComponent<PlayerController> ();
//		if (player && Input.GetKey (player.slide)) return;

		bounce (rigidbody);
	}

	void OnCollisionExit2D (Collision2D collision) {
		if (collision.otherCollider != trigger) return;

		Rigidbody2D rigidbody = collision.rigidbody;
		if (!rigidbody || rigidbody.IsTouching (trigger)) return;

//		PlayerController player = rigidbody.GetComponent<PlayerController> ();
//		if (player && Input.GetKey (player.slide)) return;

//		bounce (rigidbody);
		bouncedRigidbodies.Remove (rigidbody);
	}

	public void bounce (Rigidbody2D rigidbody) {
		if (bouncedRigidbodies.Contains (rigidbody)) return;

		JumpPadBounceEvent newEvent = new JumpPadBounceEvent (this, rigidbody);
		newEvent.call ();
		if (newEvent.isCancelled) return;

		bouncedRigidbodies.Add (rigidbody);
		rigidbody.AddForce (new Vector2 (0, bounciness), ForceMode2D.Impulse);

		timer = FXTime;
		particleFX1.Play ();
		particleFX2.Play ();
		if (SFX) SFX.Play ();
	}
}
