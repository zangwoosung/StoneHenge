using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayAndNightCycle : MonoBehaviour
{
    public Volume skyVolume;
    public Light directionalLight; // Your Sun light
    public float cycleDuration = 60f; // Seconds for full cycle

    private float time;

    void Update()
    {
        time += Time.deltaTime;
        float normalizedTime = (time % cycleDuration) / cycleDuration;

        // Simulate sky rotation (0 to 360 degrees)
        float rotation = normalizedTime * 360f;

        // Simulate exposure (brightest at noon, darkest at midnight)
        float exposure = Mathf.Lerp(-2f, 2f, Mathf.Sin(normalizedTime * Mathf.PI * 2f) * 0.5f + 0.5f);

        if (skyVolume.profile.TryGet<HDRISky>(out var hdriSky))
        {
            hdriSky.rotation.overrideState = true;
            hdriSky.exposure.overrideState = true;

            hdriSky.rotation.value = rotation;
            hdriSky.exposure.value = exposure;

            HDRenderPipeline hdPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
            hdPipeline?.RequestSkyEnvironmentUpdate();
        }

        // Optional: Animate directional light intensity and angle
       // directionalLight.transform.rotation = Quaternion.Euler(new Vector3((normalizedTime * 360f) - 90f, 0f, 0f));
       // directionalLight.intensity = Mathf.Clamp01(Mathf.Sin(normalizedTime * Mathf.PI * 2f)) * 1.5f;
    }
}
