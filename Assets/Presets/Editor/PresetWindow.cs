using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PresetWindow : EditorWindow {
	Vector2 scrollPosition;
	GUIStyle style = new GUIStyle();

	void Awake() {
	
		titleContent.text = "Preset Manager";

		Texture2D texture = new Texture2D(1, 1);
		style.normal.background = texture;
	}
		
	void OnGUI () {

		var manager = PresetsManager.sharedManager ();

		EditorGUILayout.BeginVertical ();
		scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);

		int count = manager.Count;

		for (int index = 0; index < count; ++index) {

			var info = manager.PresetAtIndex (index);
			if (index % 2 == 1) {
				EditorGUILayout.BeginHorizontal (style);
			} else {
				EditorGUILayout.BeginHorizontal ();
			}

			GUILayout.Label (info.PresetName);

			GUILayout.FlexibleSpace ();

			if (GUILayout.Button ("Remove")) {
			}

			if (GUILayout.Button ("Apply")) {
			}

			EditorGUILayout.EndHorizontal ();
		}
			
		EditorGUILayout.EndScrollView ();
		EditorGUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("New",  GUILayout.MinWidth(80))) {
		}

		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndVertical ();
	}

	public static void ShowPresetWindow () {
		var window = (PresetWindow)EditorWindow.GetWindow (typeof (PresetWindow));

		window.Show();
	}

}
