using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PresetsManager  {
	const string savePath = "Assets/Presets/presets.asset";

	private PresetInfoCollection m_presets;
	private static PresetsManager m_sharedManager = null;

	protected PresetInfoCollection Presets {
		get {
			if (m_presets == null) {
				m_presets = ScriptableObject.CreateInstance<PresetInfoCollection>();
			}
			return m_presets;
		}
	}

	public static PresetsManager sharedManager() {
		if (m_sharedManager == null) {
			m_sharedManager = new PresetsManager ();

			m_sharedManager.LoadPreset ();
		}
	
		return m_sharedManager;
	}


	public void ScanScene() {
	


	}


	public void LoadPreset() {
		LoadPreset (savePath);
	}

	public void SavePreset() {
		Debug.Log ("SavePreset");
		SavePreset (savePath);
	}

	public void LoadPreset(string path) {
		m_presets = AssetDatabase.LoadAssetAtPath<PresetInfoCollection> (path);
	}

	public void SavePreset(string path) {
		Debug.Log ("GetAssetPath:=" +AssetDatabase.GetAssetPath (Presets) );
	
		if (m_presets == null)
			return;

		if (AssetDatabase.GetAssetPath (Presets) != path) {
			AssetDatabase.CreateAsset (Presets, path);
		} else {
			EditorUtility.SetDirty (m_presets);
		}
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
	}
		
	public PresetInfo PresetInfoForID(int presetID) {
		return Presets.FindByID(presetID);
	}


	public PresetInfo CreateNewPreset(string name) {
		if (name == null || name.Length == 0)
			return null;

		if (Presets.ContainPresetInfoWithName (name))
			return null;

		PresetInfo preset = new PresetInfo (name, m_presets.MaxPresetID + 1);
		return preset;
	}



	public void AddPreset(PresetInfo info) {
		if (info != null) {
			Presets.Add (info);
		}
	}

	public void RemovePreset(PresetInfo info) {
		Presets.Remove (info);
	}


	const string UniqueNamePrefix = "New Preset";
	public string UniqueNameForPreset() {
		if (Count == 0) {
			return UniqueNamePrefix;
		}

		int index = 1;

		string name = UniqueNameForPreset (UniqueNamePrefix, index);

		while (Presets.ContainPresetInfoWithName (name)) {
			index++;
			name = UniqueNameForPreset (UniqueNamePrefix, index);
		}
		return name;
	}

	private string UniqueNameForPreset(string name, int index) {
		return string.Format ("{0} {1}", name, index);
	}
		
	public int Count {
		get {
			return Presets.Count;
		}
	}

	public PresetInfo PresetAtIndex(int index) {
		if (index >= 0 && index < m_presets.Count) {
			return Presets[index];
		}
		return null;
	}
}
