using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class PresetUtils  {

	public static void  applyPreset(this MonoBehaviour comp,  GameObjectPreset preset) {
		if (preset == null || !preset.saved) {
			return;
		}
			
		if (preset.meshFilterPreset != null) {
			var meshFilter = comp.GetComponent<MeshFilter> ();
			preset.meshFilterPreset.ApplyPresetToComponent (meshFilter);
		}

		if (preset.meshRenderPreset != null) {
			var meshRender = comp.GetComponent<MeshRenderer> ();
			preset.meshRenderPreset.ApplyPresetToComponent (meshRender);
		}
	}


	public static void fillPreset(this MonoBehaviour comp, GameObjectPreset preset) {

		if (preset == null ) {
			return;
		}

		var meshFilter = comp.GetComponent<MeshFilter> ();

		if (meshFilter) {
			MeshFilterPreset meshFilterPreset = new MeshFilterPreset();
			meshFilterPreset.FillPreset (meshFilter);
			preset.meshFilterPreset = meshFilterPreset;
		} else {
			preset.meshFilterPreset = null;
		}

		var meshRender = comp.GetComponent<MeshRenderer> ();

		if (meshRender) {
			var renderPreset = new MeshRenderPreset();
			renderPreset.FillPreset (meshRender);
			preset.meshRenderPreset = renderPreset;
		} else {
			preset.meshRenderPreset = null;
		}

		preset.saved = true;
	}

	public static bool HasPresetChanges(this MonoBehaviour behaviour, GameObjectPreset preset) {

		var meshFilter = behaviour.GetComponent<MeshFilter> ();
		if ((meshFilter == null && preset.meshFilterPreset != null) || (meshFilter != null && preset.meshFilterPreset == null)) {
			return true;
		}

		var meshFilterAndPresetAreNull = ( meshFilter == null && preset.meshFilterPreset == null);

		var meshRender = behaviour.GetComponent<MeshRenderer>();
		if ((meshRender == null && preset.meshRenderPreset != null) || (meshRender != null && preset.meshRenderPreset == null)) {
			return true;
		}

		var meshRenderAndPresetAreNull = (meshRender == null && preset.meshRenderPreset == null);

		if (meshFilterAndPresetAreNull && meshRenderAndPresetAreNull) {
			return false;
		}
			
		if (!meshFilterAndPresetAreNull && !preset.meshFilterPreset.EqualToComponent(meshFilter)) {
			return true;
		}

		if (!meshRenderAndPresetAreNull && !preset.meshRenderPreset.EqualToComponent(meshRender)) {
			return true;
		}

		return false;
	}
}
