using System;
using UnityEngine;

[Serializable]
public sealed class GameObjectPresetPair {

	[SerializeField]
	private int m_presetID;
	[SerializeField]
	private GameObjectPreset m_preset;

	public GameObjectPresetPair(GameObjectPreset preset, int presetID) {
		m_preset = preset;
		m_presetID = presetID;
	}
		
	public int PresetID {
		get {
			return m_presetID;
		}
	}

	public GameObjectPreset Preset {
		get {
			return m_preset;
		}
	}
}