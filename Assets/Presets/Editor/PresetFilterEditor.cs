using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PresetFilter))]
public class PresetFilterEditor : Editor {

	//GUILayout.Button("Create")



	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		var manager = PresetsManager.sharedManager ();

		PresetFilter filter = (PresetFilter)target;
		var currentPresetInfo = filter.ActivePresetInfo;

		string[] options = new string[manager.Count];

		int currentPresetIndex = 0; 
		for (int index = 0; index < manager.Count; ++index) {
			var info = manager.PresetAtIndex (index);
			options [index] = info.PresetName;
			if (currentPresetInfo != null && info.PresetID == currentPresetInfo.PresetID) {
				currentPresetIndex = index;
			}
		}
			
		EditorGUILayout.BeginVertical ();
		EditorGUILayout.BeginHorizontal();

		if (manager.Count > 1) {

			int popupIndex = EditorGUILayout.Popup ("Preset:", 
				                 currentPresetIndex, options, EditorStyles.popup);


			if (popupIndex != currentPresetIndex) {
				currentPresetInfo = manager.PresetAtIndex (popupIndex);
				filter.ActivePresetInfo = currentPresetInfo;
			}
		} else {
		
			if (GUILayout.Button ("New Preset")) {
				//filter.applyPreset (filter.ActivePreset);
			}
		}
			
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();

		GUI.enabled = (filter.ActivePreset != null && filter.ActivePreset.saved);

		if (GUILayout.Button ("Load")) {
			filter.applyPreset (filter.ActivePreset);
		}
			
		GUI.enabled =  filter.ActivePreset != null && filter.HasPresetChanges(filter.ActivePreset);
		if (GUILayout.Button ("Save")) {
			var preset = new GameObjectPreset ();
			filter.fillPreset(preset);

			filter.SetPreset (preset, filter.ActivePresetInfo);
		}

		GUI.enabled = true;


		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical ();

	}

}
