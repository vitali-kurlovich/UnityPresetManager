using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public sealed class PresetInfo  {
	[SerializeField]
	private string m_name;
	[SerializeField]
	private int m_presetID;

	public PresetInfo (string name, int presetID) {
		m_name = name;
		m_presetID = presetID;
	}

	public int PresetID {
		get {
			return m_presetID;
		}
	}

	public string PresetName {
		get {
			return m_name;
		}
	}
		
	public override bool Equals (object obj)
	{
		if (obj != null && obj is PresetInfo) {
			var info = (PresetInfo)obj;
			return m_presetID == info.m_presetID && m_name == info.m_name;
		}
		return false;
	}

	public override int GetHashCode ()
	{
		return m_name.GetHashCode() ^ m_presetID;
	}

	public override string ToString ()
	{
		return string.Format ("[PresetInfo: PresetID={0}, PresetName={1}]", PresetID, PresetName);
	}
}
