using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PresetFilter))]
public class PresetFilterEditor : Editor {

	public override void OnInspectorGUI()
	{
		var manager = PresetsManager.sharedManager ();

		PresetFilter filter = (PresetFilter)target;
		var currentPresetInfo = filter.ActivePresetInfo;

		string[] options = new string[manager.Count + 2];
		options [0] = "None";
		options [manager.Count+1] = "New";

		int currentPresetIndex = 0; 
		for (int index = 0; index < manager.Count; ++index) {
			var info = manager.PresetAtIndex (index);
			options [index+1] = info.PresetName;
			if (currentPresetInfo != null && info.PresetID == currentPresetInfo.PresetID) {
				currentPresetIndex = index + 1;
			}
		}
	
		EditorGUILayout.BeginVertical ();
		EditorGUILayout.BeginHorizontal();

		int popupIndex = EditorGUILayout.Popup ("Preset:", 
				                 currentPresetIndex, options, EditorStyles.popup);

		if (popupIndex != currentPresetIndex) {
			if (popupIndex == 0) {
				filter.ActivePresetInfo = null;
			} else if (popupIndex == manager.Count + 1) {
				NewPrestWizard.ShowNewPrestWizard ();
			} else {
				currentPresetInfo = manager.PresetAtIndex (popupIndex - 1);
				filter.ActivePresetInfo = currentPresetInfo;
			}
		}

		EditorGUILayout.EndHorizontal();

		if (filter.ActivePresetInfo != null) {

			EditorGUILayout.BeginHorizontal ();

			GUI.enabled = (filter.ActivePreset != null && filter.ActivePreset.saved);

			if (GUILayout.Button ("Load")) {
				filter.applyPreset (filter.ActivePreset);
			}
			
			GUI.enabled = filter.ActivePreset != null && filter.HasPresetChanges (filter.ActivePreset);
			if (GUILayout.Button ("Save")) {
				var preset = new GameObjectPreset ();
				filter.fillPreset (preset);

				filter.SetPreset (preset, filter.ActivePresetInfo);
			}

			GUI.enabled = true;

			EditorGUILayout.EndHorizontal ();
		}


		EditorGUILayout.EndVertical ();
	}

}
