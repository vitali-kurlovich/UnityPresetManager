using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public sealed class MeshFilterPreset : AbstractPreset {

	[SerializeField]
	private Mesh m_sharedMesh;

	public Mesh SharedMesh {
		get {
			return m_sharedMesh;
		}
	}

	public override void FillPreset(Component component) {
		if (component == null || !(component is MeshFilter))
			return;
		FillWithMeshFilter ((MeshFilter)component);
	}

	public override bool EqualToComponent(Component component) {
		if (component == null || !(component is MeshFilter))
			return false;
		return EqualToMeshFilter((MeshFilter)component);
	}

	public override bool ApplyPresetToComponent(Component component) {
		if (component == null || !(component is MeshFilter))
			return false;
		return ApplyPresetToMeshFilter ((MeshFilter)component);
	}


	public bool EqualToMeshFilter(MeshFilter meshFilter) {
		return (meshFilter != null && m_sharedMesh == meshFilter.sharedMesh);
	}
		
	public void FillWithMeshFilter(MeshFilter meshFilter) {
		if (meshFilter == null) {
			m_sharedMesh = null;
		} else {
			m_sharedMesh = meshFilter.sharedMesh;
		}
	}

	public bool ApplyPresetToMeshFilter(MeshFilter meshFilter) {
		meshFilter.sharedMesh = m_sharedMesh;
		return true;
	}
}
