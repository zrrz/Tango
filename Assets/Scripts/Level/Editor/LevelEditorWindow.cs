using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelEditorWindow : EditorWindow {

	LevelData currentLevel;

	public static void Init (LevelData level) {
		// Get existing open window or if none, make a new one:
		LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow (typeof (LevelEditorWindow));
		window.Show();
		window.currentLevel = level;
	}

	void OnGUI () {
		if(currentLevel == null) {
			if(LevelSelectorWindow.currentLevel == null) {
				EditorGUILayout.LabelField("Click edit on level editor window again.");
				return;
			} else {
				currentLevel = LevelSelectorWindow.currentLevel;
			}
		}
		EditorGUILayout.LabelField(currentLevel.name);

		for(int x = 0; x < currentLevel.width; x++) {
			EditorGUILayout.BeginHorizontal();
			for(int y = 0; y < currentLevel.height; y++) {
				int index = y * currentLevel.width + x;
				string type = "";
				switch(currentLevel.levelObjects[index].type) {
					case LevelObject.Type.Floor:
						type = "O";
						break;
					case LevelObject.Type.Hole:
						type = "X";
						break;
					case LevelObject.Type.Box:
						type = "B";
						break;
					case LevelObject.Type.Button:
						type = "i";
						break;
					case LevelObject.Type.Teleport:
						type = "T";
						break;
					default:
						break;
				}
				if(GUILayout.Button(type)) {
					switch(currentLevel.levelObjects[index].type) {
						case LevelObject.Type.Floor:
							currentLevel.levelObjects[index].type = LevelObject.Type.Hole;
							break;
						case LevelObject.Type.Hole:
							currentLevel.levelObjects[index].type = LevelObject.Type.Box;
							break;
						case LevelObject.Type.Box:
							currentLevel.levelObjects[index].type = LevelObject.Type.Button;
							break;
						case LevelObject.Type.Button:
							currentLevel.levelObjects[index].type = LevelObject.Type.Teleport;
							break;
						case LevelObject.Type.Teleport:
							currentLevel.levelObjects[index].type = LevelObject.Type.Floor;
							break;
					}
				}
				EditorUtility.SetDirty(currentLevel);
				
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}
