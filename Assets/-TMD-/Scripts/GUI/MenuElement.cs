using System;
using UnityEngine;
using System.Collections;

public enum MenuElementState : int {Deselected = 0, Selected = 1, Hidden = 2}

public class MenuElement : MonoBehaviour {

	public CustomSlideAnimator animator;
	private RectTransform element;

	public MenuElementState state;
	public bool isBeingShown;
	public bool isResetting;

	public MenuElement parent;
	public MenuElement header;
	public MenuElement[] children;

	public bool showNormal = false;
	public FixedVector2 normal;
	private Vector2 normalPos;

	public bool showSelected = true;
	public FixedVector2 selected;
	private Vector2 selectedPos;

	public bool showHidden = false;
	public FixedVector2 hidden;
	private Vector2 hiddenPos;

	public void Start () {
		element = GetComponent<RectTransform> ();
		animator.setRectTransform (element);
		normalPos = normal.fixTo (element.anchoredPosition);
		selectedPos = selected.fixTo (element.anchoredPosition);
		hiddenPos = hidden.fixTo (element.anchoredPosition);
		animator.setTarget (normalPos);
	}

	// for Children

	public void toggleChildren () {
		if (header) header.toggleElement ();
		foreach (MenuElement child in children) {
			child.toggleElement ();
		}
		if (parent) {
			if (header && header.state == MenuElementState.Selected) {
				parent.hideChildrenExcept (this);
			} else if (header && header.state == MenuElementState.Deselected) {
				parent.unhideChildrenExcept (this);
			}
		}
	}

	public void hideChildrenExcept (MenuElement notToHide) {
		if (header) header.hideElement ();
		foreach (MenuElement child in children) {
			if (child != notToHide) child.hideElement ();
		}
	}
	public void unhideChildrenExcept (MenuElement notToUnhide) {
		if (header) header.unhideElement ();
		foreach (MenuElement child in children) {
			child.unhideElement ();
		}
	}

	public void resetAllChildren () {
		Debug.Log ("resetAll");
		if (header) header.resetElement ();
		if (header) header.resetAllChildren ();
		foreach (MenuElement child in children) {
			child.resetElement ();
			child.resetAllChildren ();
		}
	}

	// for Self

	public void toggleElement () {
		switch (state) {
		case (MenuElementState)0:
			selectElement ();
			break;
		case (MenuElementState)1:
			deselectElement ();
			break;
		}
	}

	public void selectElement () {
		state = MenuElementState.Selected;
		animator.setTarget (selectedPos);
	}
	public void deselectElement () {
		state = MenuElementState.Deselected;
		animator.setTarget (normalPos);
	}

	public void hideElement () {
		state = MenuElementState.Hidden;
		animator.setTarget (hiddenPos);
	}
	public void unhideElement () {
		state = MenuElementState.Selected;
		animator.setTarget (selectedPos);
	}

	public void enableElement () {
		isBeingShown = true;
		element.localScale = Vector2.one;
	}
	public void disableElement () {
		isBeingShown = false;
		element.localScale = Vector2.zero;
	}

	public void resetElement () {
		deselectElement ();
		isResetting = true;
//		animator.goToTarget ();
	}

	// Update

	void Update () {
		animator.update (Time.realtimeSinceStartup);

		if (isResetting && animator.onTarget) isResetting = false;
		if (!isResetting) enableElement ();
		if (!showNormal && state == MenuElementState.Deselected && animator.onTarget) {
			disableElement ();
		}
		if (!showSelected && state == MenuElementState.Selected && animator.onTarget) {
			disableElement ();
		}
		if (!showHidden && state == MenuElementState.Hidden && animator.onTarget) {
			disableElement ();
		}
	}
}
