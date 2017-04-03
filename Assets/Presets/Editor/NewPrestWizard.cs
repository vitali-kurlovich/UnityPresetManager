using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NewPrestWizard : EditorWindow {

	private string m_infoName = null;

	public static void ShowNewPrestWizard () {
		var window = EditorWindow.GetWindow <NewPrestWizard>();
		window.Show();
		var manager = PresetsManager.sharedManager ();
		window.m_infoName = manager.UniqueNameForNewPreset();
	}
		
	private void OnLostFocus() {
	
		Focus ();
	}


	void OnGUI () {

		var manager = PresetsManager.sharedManager ();
		EditorGUILayout.BeginVertical ();
		m_infoName = GUILayout.TextField (m_infoName);

		EditorGUILayout.BeginHorizontal ();

		GUILayout.FlexibleSpace ();

		if (GUILayout.Button ("Cancel")) {
			//manager.SetActivePressetToScene (info);
			Close();
		}

		GUI.enabled = m_infoName != null && m_infoName.Length > 0 && !manager.ContainPresetInfoWithName(m_infoName);
		if (GUILayout.Button ("Create")) {
			var info = manager.CreateNewPreset (m_infoName);
			manager.AddPreset (info);
			manager.SavePreset ();

			manager.SetActivePressetToScene (info);
			Close();
		}

		GUI.enabled = true;

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.EndVertical ();
	}

}
