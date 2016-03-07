using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelObject {

	public enum Type {
		Floor, Hole, Button, Teleport, Box
	}

	[SerializeField]
	public ColorComponent color;

	[SerializeField]
	public Type type;
}
