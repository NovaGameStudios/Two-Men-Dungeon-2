using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SceneInfo {
	public string name;

	public enum SceneType {
		Menu, Level, Other
	}
	public SceneType type;


}