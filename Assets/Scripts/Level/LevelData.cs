using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData : ScriptableObject {

	[SerializeField]
	public LevelObject[] levelObjects;

	[SerializeField]
	public int width, height;
}
