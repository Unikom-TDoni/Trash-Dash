using UnityEngine;
using Group8.TrashDash.TimeManager;

[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset lightningPreset;

    [Header("Variables")]
    [SerializeField, Range(0, 1)] private float timeOfDay;

    private void Update()
    {
        if (lightningPreset == null) return;

        if (Application.isPlaying)
        {
            timeOfDay = (timeManager.CurrentTime / timeManager.TimePerDay) % timeManager.TimePerDay;
        }
        UpdateLighting(timeOfDay);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightningPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightningPreset.FogColor.Evaluate(timePercent);

        if (directionalLight == null) return;
        directionalLight.color = lightningPreset.DirectionalColor.Evaluate(timePercent);
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0f));
    }

    private void OnValidate()
    {
        if (directionalLight == null) directionalLight = RenderSettings.sun;
    }
}
