using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Controls", order = 1)]
public class Controls : ScriptableObject {

	[Header("Player 1")]
	public KeyCode p1Jump;
	public KeyCode p1Slide;
	public KeyCode p1Left;
	public KeyCode p1Right;
	[Space(10)]
	[Header("Player 2")]
	public KeyCode p2Jump;
	public KeyCode p2Slide;
	public KeyCode p2Left;
	public KeyCode p2Right;
	[Space(10)]
	[Header("Menu")]
	public KeyCode enter;
	public KeyCode back;
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	[Space(10)]
	[Header("InGame")]
	public KeyCode pause;
	public KeyCode timePause;
	public KeyCode restart;

}