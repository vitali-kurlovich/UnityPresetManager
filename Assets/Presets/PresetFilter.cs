using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[ExecuteInEditMode]
public class PresetFilter : MonoBehaviour {
	
	[SerializeField]
	private List<GameObjectPresetPair> m_presets;
	[SerializeField]
	private GameObjectPresetPair m_activePreset;

	public PresetInfo ActivePresetInfo {
		get {
			if (m_activePreset == null)
				return null;
			
			return PresetsManager.sharedManager().PresetInfoForID (m_activePreset.PresetID);
		}

		set {
			if (value == null) {
				m_activePreset = null;
				return;
			}
			if (m_presets != null) {
				foreach (GameObjectPresetPair p in m_presets) {
					if (p.PresetID == value.PresetID) {
						m_activePreset = p;
						return;
					}
				}
			}
			m_activePreset = new GameObjectPresetPair (null, value.PresetID);
		}
	}

	public GameObjectPreset ActivePreset {
		get {
			if (m_activePreset == null)
				return null;
			return m_activePreset.Preset;
		}
	}


	public List<int> SupportedPresetIDs {
		get {

			if (m_presets == null || m_presets.Count < 1)
				return null;

			List<int> ids = new List<int> ();

			foreach (GameObjectPresetPair pair in m_presets) {
				ids.Add (pair.PresetID);
			}

			return ids;
		}
	}
		
	public void RemovePreset(PresetInfo info) {
		List<GameObjectPresetPair> objectForRemove = new List<GameObjectPresetPair>(); 
		foreach (GameObjectPresetPair p in m_presets) {
			if (p.PresetID == info.PresetID) {
				objectForRemove.Add(p);
			}
		}

		foreach (GameObjectPresetPair p in objectForRemove) {
			m_presets.Remove(p);
		}
			
		if (m_activePreset != null && m_activePreset.PresetID == info.PresetID) {
			m_activePreset = new GameObjectPresetPair (null, info.PresetID);
		}
	}


	public void SetPreset(GameObjectPreset preset, PresetInfo info) {

		if (preset == null || info == null )
			return;

		if (m_presets == null) {
			m_presets = new List<GameObjectPresetPair> ();
		}
			
		RemovePreset(info);

		var pair = new GameObjectPresetPair (preset, info.PresetID);
		m_presets.Add (pair);

		if (m_activePreset != null && m_activePreset.PresetID == info.PresetID) {
			m_activePreset = pair;
		}
	}

	public GameObjectPreset PresetForInfo(PresetInfo info) {

		if (m_presets == null) {
			return null;
		}

		foreach (GameObjectPresetPair p in m_presets) {
			if (p.PresetID == info.PresetID) {
				return p.Preset;
			}
		}
			
		return null;
	}
}
