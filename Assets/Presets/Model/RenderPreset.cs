using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class RenderPreset : AbstractPreset {
	[SerializeField]
	private bool m_enabled;
	[SerializeField]
	private UnityEngine.Rendering.ShadowCastingMode m_castShadows;
	public bool receiveShadows;

	public MotionVectorGenerationMode motionVectorGenerationMode;

	public Material[] sharedMaterials;

	public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
	public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;

	public Transform probeAnchor;

	public bool Enabled {
		get { return m_enabled; }
	}

	public UnityEngine.Rendering.ShadowCastingMode CastShadows {
		get { return m_castShadows; }
	}
		
	public override void FillPreset (Component component) {
		if (component == null || !(component is Renderer))
			return;
		FillPresetWithRender ((Renderer)component);
	}

	public override bool EqualToComponent (Component component){
		if (component == null || !(component is Renderer))
			return false;
		return EqualToRender ((Renderer)component);
	}
		
	public bool EqualToRender (Renderer render) {
		return 
			m_enabled == render.enabled &&
		m_castShadows == render.shadowCastingMode &&
		receiveShadows == render.receiveShadows &&
		motionVectorGenerationMode == render.motionVectorGenerationMode &&
		lightProbeUsage == render.lightProbeUsage &&
		reflectionProbeUsage == render.reflectionProbeUsage &&
		probeAnchor == render.probeAnchor &&
		ArrayUtility.ArrayEquals(sharedMaterials, render.sharedMaterials);
	}

	public void FillPresetWithRender (Renderer render) {
		m_enabled = render.enabled;
		m_castShadows = render.shadowCastingMode;
		receiveShadows = render.receiveShadows;
		motionVectorGenerationMode = render.motionVectorGenerationMode;
		lightProbeUsage = render.lightProbeUsage;
		reflectionProbeUsage = render.reflectionProbeUsage;
		probeAnchor = render.probeAnchor;
		sharedMaterials = render.sharedMaterials;
	}

	public override bool ApplyPresetToComponent(Component component) {
		if (component == null || !(component is Renderer))
			return false;
		return ApplyPresetToRenderer ((Renderer)component);
	}
		
	public bool ApplyPresetToRenderer(Renderer render) {
		render.enabled = m_enabled;
		render.shadowCastingMode = m_castShadows;
		render.receiveShadows = receiveShadows;
		render.motionVectorGenerationMode = motionVectorGenerationMode;
		render.lightProbeUsage = lightProbeUsage;
		render.reflectionProbeUsage = reflectionProbeUsage;
		render.probeAnchor = probeAnchor;
		render.sharedMaterials = sharedMaterials;
		return true;
	}
}