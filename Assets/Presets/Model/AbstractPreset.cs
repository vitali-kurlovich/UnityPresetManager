
using UnityEngine;
using System;


public abstract class AbstractPreset  {
	abstract public void FillPreset (Component component);
	abstract public bool ApplyPresetToComponent(Component component);

	abstract public bool EqualToComponent(Component component);
}
