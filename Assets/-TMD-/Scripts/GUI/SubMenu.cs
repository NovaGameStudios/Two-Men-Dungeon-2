using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SubMenu", order = 1)]
public class SubMenu : MenuNode {
	public MenuNode[] children;
}