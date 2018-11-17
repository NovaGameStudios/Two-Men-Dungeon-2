using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaFader : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public InOutAnimator animator;

	void Start () {
		if (!spriteRenderer) GetComponent<Image> ();
		animator.Init ();
	}

	public CustomAsyncOperation fadeIn () {
		return animator.In ();
	}

	public CustomAsyncOperation fadeOut () {
		return animator.Out ();
	}

	void Update () {
		animator.Update ();
		spriteRenderer.SetAlpha (animator.value);
	}
}