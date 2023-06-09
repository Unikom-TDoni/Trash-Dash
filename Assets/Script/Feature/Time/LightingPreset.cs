using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Lighting/Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
