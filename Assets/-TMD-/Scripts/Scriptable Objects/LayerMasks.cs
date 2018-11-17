using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LayerMasks", order = 1)]
public class LayerMasks : ScriptableObject {

	public LayerMask ground;
	public LayerMask jumpPad;
	public LayerMask slidable;
	public LayerMask wall;
	public LayerMask death;
//	public LayerMask crushMask;

}