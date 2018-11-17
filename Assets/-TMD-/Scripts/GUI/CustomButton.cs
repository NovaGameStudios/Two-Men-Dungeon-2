using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[ExecuteInEditMode]
public class CustomButton : ButtonManager {

	public BoxCollider2D boxCollider;

	void Update () {
		boxCollider.size = (transform as RectTransform).sizeDelta;
	}

	/*
//	void Start () { if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) enabled = false; }

	public ButtonDefaults defaults;

	[TextArea (3, 10)] public string textStr;

	public bool update;

	public Image image;
	public Button createdButton;
	public HorizontalLayoutGroup layout;
	public ContentSizeFitter buttonFitter;

	public GameObject textObject;
	public Text text;
	public Outline textOutline;
	public Shadow textShadow;
	public ContentSizeFitter textFitter;

	public Rigidbody2D rigid;
	public BoxCollider2D coll;


	void createImage () {
		image = this.GetOrAddComponent<Image> ();
		image.type = Image.Type.Sliced;
	}
	void createButton () {
		createdButton = this.GetOrAddComponent<Button> ();
		createdButton.transition = Selectable.Transition.SpriteSwap;
	}
	void createLayout () {
		layout = this.GetOrAddComponent<HorizontalLayoutGroup> ();
		layout.childAlignment = TextAnchor.MiddleCenter;
	}
	void createButtonFitter () {
		buttonFitter = this.GetOrAddComponent<ContentSizeFitter> ();
		buttonFitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
		buttonFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
	}


	void createTextObject () {
		textObject = new GameObject ("Text", typeof (RectTransform));
		textObject.transform.SetParent (transform);
		textObject.transform.localPosition = Vector3.zero;
		textObject.transform.localScale = new Vector3 (1, 1, 1);
	}
	void createText () {
		text = textObject.GetOrAddComponent<Text> ();
		text.alignment = TextAnchor.MiddleCenter;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.verticalOverflow = VerticalWrapMode.Overflow;
	}
	void createTextFitter () {
		textFitter = textObject.GetOrAddComponent<ContentSizeFitter> ();
		textFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		textFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
	}
	void createTextOutline () {
		textOutline = textObject.GetOrAddComponent<Outline> ();
	}
	void createTextShadow () {
		textShadow = textObject.AddComponent<Shadow> ();
	}

	void Awake () {
		create ();
	}

	void Start () {
		create ();
	}

	#if UNITY_EDITOR
	void Update () {
		
		if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) return;
		create ();
	}
	#endif
	private bool lol = true;
	void create () {

		if (update) {
			buttonFitter.enabled = false;
			buttonFitter.enabled = true;
			update = false;
		}
		if (!defaults) return;

		if (!image) createImage ();
		if (!createdButton) createButton ();
		if (!layout) createLayout ();
		if (!buttonFitter) createButtonFitter ();

		if (!textObject) createTextObject ();
		if (!text) createText ();
		if (!textFitter) createTextFitter ();
		if (!textOutline) createTextOutline ();
		if (!textShadow) createTextShadow ();

		if (!rigid) rigid = gameObject.AddComponent<Rigidbody2D> ();
		if (!coll) coll = gameObject.AddComponent<BoxCollider2D> ();

		image.sprite = defaults.sprite;
		createdButton.spriteState = defaults.spriteState;

		layout.padding = defaults.padding;

		text.text = textStr;
		text.font = defaults.font;
		text.fontSize = defaults.fontSize;

		textShadow.effectColor = defaults.shadowColor;
		textShadow.effectDistance = defaults.shadowDistance;

		rigid.gravityScale = defaults.gravityScale;
//		rigid.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

		coll.sharedMaterial = defaults.material;
		coll.size = (transform as RectTransform).sizeDelta;
	} */
}