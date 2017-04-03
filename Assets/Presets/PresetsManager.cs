using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PresetsManager  {
	const string savePath = "Assets/Presets/presets.asset";

	private PresetInfoCollection m_presets;
	private static PresetsManager m_sharedManager = null;

	private SceneScanner m_sceneScanner = new SceneScanner();

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
			m_sharedManager.ScanScene ();
		}
		return m_sharedManager;
	}
		
	public void ScanScene() {
		var result = m_sceneScanner.Scan ();

		var ids = new HashSet<int> ();
		foreach (var filter in result) {
			var presetIDs = filter.SupportedPresetIDs;

			if (presetIDs == null)
				continue;
			foreach (int id in presetIDs) {
				ids.Add (id);
			}
		}
	}

	public void SetActivePressetToScene(PresetInfo info) {
		var filters = m_sceneScanner.Scan ();

		foreach (PresetFilter pf in filters) {
			pf.ActivePresetInfo = info;
		}
	}

	public void ApplyPressetToScene(PresetInfo info) {

		var filters = m_sceneScanner.Scan ();

		foreach (PresetFilter pf in filters) {
			pf.ActivePresetInfo = info;
			pf.applyPreset (pf.ActivePreset);
		}
	}
		
	public List<PresetFilter> FilterPresetFilterByPresetID(List<PresetFilter> list, int presetID) {
		var result = new List<PresetFilter> ();
		foreach (PresetFilter pf in list) {

		}
		return  result;
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

	public bool ContainPresetInfoWithName(string name) {
		if (name == null)
			return false;

		return Presets.ContainPresetInfoWithName (name);
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
	const string UniqueUntitledNamePrefix = "Untitled";

	public string UniqueNameForNewPreset() {
		return UniqueNameForPreset (UniqueNamePrefix);
	}

	public string UniqueNameForUntitledPreset() {
		return UniqueNameForPreset (UniqueUntitledNamePrefix);
	}

	public string UniqueNameForPreset(string prefix) {
		int index = 0;
		string name = FormatNameForPreset (prefix, index);

		while (Presets.ContainPresetInfoWithName (name)) {
			index++;
			name = FormatNameForPreset (prefix, index);
		}
		return name;
	}

	private string FormatNameForPreset(string name, int index) {
		if (index == 0)
			return name;
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