using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScanner  {


	 

	public List<PresetFilter> Scan() {
		var scene = SceneManager.GetActiveScene ();
		return Scan (scene);
	}
		
	public List<PresetFilter> Scan(Scene scene) {
		var list = new List<PresetFilter> ();

		var rootsGameObjects = scene.GetRootGameObjects ();

		foreach (GameObject go in rootsGameObjects) {
			var comps = go.GetComponentsInChildren<PresetFilter> ();

			foreach (PresetFilter pf in comps ) {
				list.Add (pf);
			}
		}
			
		return list;
	}
}
