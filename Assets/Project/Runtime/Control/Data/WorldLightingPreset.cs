using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName =  "Lighting Preset", menuName = "Rendering/Lighting Preset", order = 1)]
public class WorldLightingPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient SunColor;
    public Gradient moonColor;
    public Gradient FogColor;
    public AnimationCurve FogDensity;
    public AnimationCurve AtmosphereIntensity;
}
