using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class LevelSelectorWindow : EditorWindow {

	List<LevelData> levels;
	public static LevelData currentLevel;

	[MenuItem ("Level Editor/Edit")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		LevelSelectorWindow window = (LevelSelectorWindow)EditorWindow.GetWindow (typeof (LevelSelectorWindow));
		window.Show();
	}

	void OnGUI () {
		if(GUILayout.Button("Refresh")) {
			RefreshLevels();
		}
		if(GUILayout.Button("New Level")) {
			RefreshLevels(); //For some reason I need this beforehand? Maybe cleanup?
			NewLevel();
			RefreshLevels(); //Refresh for newly added level
		}
		if(levels != null) {
			for(int i = 0; i < levels.Count; i++) {
				EditorGUILayout.BeginHorizontal();
				if(levels[i] != null)
					EditorGUILayout.LabelField(levels[i].name);
				if(GUILayout.Button("Edit")) {
					EditLevel(levels[i]);
				}
				if(GUILayout.Button("-")) {
					AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(levels[i]));
					levels.RemoveAt(i);
					i--;
				}

				EditorGUILayout.EndHorizontal();
			}
		}
	}

	void EditLevel(LevelData level) {
		currentLevel = level;
		LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow (typeof (LevelEditorWindow));
		window.Close();
		LevelEditorWindow.Init(level);
	}

	void RefreshLevels() {
		if(levels == null)
			levels = new List<LevelData>();
		levels.Clear();
		string localPath = "Assets/Levels/";
		string[] data = Directory.GetFiles(localPath);//AssetDatabase.LoadAllAssetsAtPath("Assets/Levels/");
		foreach(string fileName in data)
		{
			LevelData level = AssetDatabase.LoadAssetAtPath<LevelData>(fileName);
			if(level != null)
				levels.Add(level);
		}
	}

	void NewLevel() {
		LevelData newLevel = ScriptableObject.CreateInstance<LevelData>();
		newLevel.width = newLevel.height = 10;
		newLevel.levelObjects = new LevelObject[newLevel.width * newLevel.height];

		for(int x = 0; x < newLevel.width; x++) {
			for(int y = 0; y < newLevel.height; y++) {
				int index = y * currentLevel.width + x;
				newLevel.levelObjects[index] = new LevelObject();
				newLevel.levelObjects[index].type = LevelObject.Type.Floor;
			}
		}
		string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Levels/Level 1.asset");
		AssetDatabase.CreateAsset(newLevel, path);
		newLevel.name = Path.GetFileNameWithoutExtension(path);
		EditorUtility.SetDirty(newLevel);
	}
}
