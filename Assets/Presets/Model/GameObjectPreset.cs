using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class GameObjectPreset  {
	public bool saved;

	public MeshRenderPreset meshRenderPreset;
	public MeshFilterPreset meshFilterPreset;

	public override string ToString ()
	{
		return string.Format ("Preset: saved = {0}", (saved ? "yes" : "no"));
	}
}
