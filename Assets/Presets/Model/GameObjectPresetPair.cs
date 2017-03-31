using System;
using UnityEngine;

[Serializable]
public sealed class GameObjectPresetPair {
	public GameObjectPresetPair(GameObjectPreset preset, int presetID) {
		m_preset = preset;
		m_presetID = presetID;
	}
	[SerializeField]
	private int m_presetID;
	[SerializeField]
	private GameObjectPreset m_preset;

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