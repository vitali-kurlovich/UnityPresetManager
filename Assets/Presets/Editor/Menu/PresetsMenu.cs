using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class PresetsMenu  {
	[MenuItem ("Window/Preset", false, 2120)]
	static void OpenPresetManager () {
		PresetWindow.ShowPresetWindow ();
	}

}
