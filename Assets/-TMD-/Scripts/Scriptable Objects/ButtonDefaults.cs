using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ButtonDefaults", order = 1)]
public class ButtonDefaults : ScriptableObject {
	
	public RectOffset padding;
//	public float factor;
	public Font font;
	public int fontSize;
	public Sprite sprite;
	public SpriteState spriteState;
	public Color shadowColor;
	public Vector2 shadowDistance;
	public PhysicsMaterial2D material;
	public float gravityScale;

}
