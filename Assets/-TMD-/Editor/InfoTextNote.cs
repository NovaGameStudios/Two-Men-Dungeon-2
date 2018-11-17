using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************
*   This file let you add text into the inspector
* has a 2° reference file in Assets "Editor" folder,
* name:  AddInspectorText
**************************************************/

public class InfoTextNote : MonoBehaviour
{

	public bool isReady = true;
	public string TextInfo = "Text here... " +
		"/n Press Lock when finish.";

	public void SwitchToggle ()
	{
		isReady = !isReady;
	}

	void Start ()
	{
		this.enabled = false; // Disable thi component when game start
	}
}