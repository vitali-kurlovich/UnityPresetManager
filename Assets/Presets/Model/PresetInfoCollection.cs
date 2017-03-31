
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class PresetInfoCollection : UnityEngine.Object
{
	[SerializeField]
	private List<PresetInfo> m_items;

	public int Count {
		get {
			if (m_items == null)
				return 0;
			return m_items.Count;
		}
	}

	public PresetInfo this[int index] {
		get {
			return m_items[index];
		}
	}

	public void Add(PresetInfo info) {

		if (m_items == null) {
			m_items = new List<PresetInfo>();
		}

		m_items.Add (info);
	}

	public void Remove(PresetInfo info) {
		if (m_items == null)
			return;
		m_items.Remove (info);
	}

	public PresetInfo FindByName(string name) {
		if (m_items == null)
			return null;
		foreach (PresetInfo p in m_items) {
			if (p.PresetName.Equals(name)) {
				return p;
			}
		}
		return null;
	}

	public PresetInfo FindByID(int presetID) {
		if (m_items == null)
			return null;
		foreach (PresetInfo p in m_items) {
			if (p.PresetID == presetID) {
				return p;
			}
		}
		return null;
	}

	public bool ContainPresetInfoWithName(string name) {
		return FindByName(name) != null;
	}

	public bool ContainPresetInfoWithID(int presetID) {
		return FindByID(presetID) != null;
	}

	public override string ToString ()
	{
		if (Count > 0) {
			return string.Format ("[PresetInfoCollection: Count={0}, Item={1}]", Count, m_items);
		}
		return string.Format ("[PresetInfoCollection: Count={0}]", Count);
	}
}