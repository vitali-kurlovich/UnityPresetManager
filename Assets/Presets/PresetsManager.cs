using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PresetsManager  {
	
	private PresetInfoCollection m_presets = new PresetInfoCollection ();
	private static PresetsManager m_sharedManager = null;

	public static PresetsManager sharedManager() {
		if (m_sharedManager == null) {
			m_sharedManager = new PresetsManager ();

			m_sharedManager.AddNewPreset ("Preset 1");
			m_sharedManager.AddNewPreset ("Preset 2");
			m_sharedManager.AddNewPreset ("Preset 3");
			m_sharedManager.AddNewPreset ("Preset 4");

			Debug.Log (m_sharedManager.m_presets);

			m_sharedManager.SavePreset ();
		}
	
		return m_sharedManager;
	}

	const string savePath = "Assets/Presets/presets.asset";

	public void LoadPreset() {
		LoadPreset (savePath);
	}

	public void SavePreset() {
		SavePreset (savePath);
	}

	public void LoadPreset(string path) {
		m_presets = Resources.Load<PresetInfoCollection> (path);
	}

	public void SavePreset(string path) {
		if (m_presets != null) {
			AssetDatabase.CreateAsset (m_presets, path);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
		}
	}
		
	public PresetInfo PresetInfoForID(int presetID) {
		return m_presets.FindByID(presetID);
	}

	public PresetInfo AddNewPreset(string name) {
		if (name == null || name.Length == 0)
			return null;

		if (m_presets.ContainPresetInfoWithName (name))
			return null;

		PresetInfo preset = new PresetInfo (name, m_presets.MaxPresetID + 1);
		m_presets.Add (preset);

		return preset;
	}

	public int Count {
		get {
			return m_presets.Count;
		}
	}

	public PresetInfo PresetAtIndex(int index) {
		if (index >= 0 && index < m_presets.Count) {
			return m_presets[index];
		}
		return null;
	}
}
