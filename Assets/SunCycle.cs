using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SunCycle : MonoBehaviour
{
    public Volume skyVolume;
    public Light directionalLight; // Assign your sun light
    public float cycleDuration = 60f; // Full day-night cycle in seconds

    private float time;

    void Update()
    {
        time += Time.deltaTime;
        float normalizedTime = (time % cycleDuration) / cycleDuration;

        // ☀️ Sky rotation and exposure
        float skyRotation = normalizedTime * 360f;
        float skyExposure = Mathf.Lerp(-2f, 2f, Mathf.Sin(normalizedTime * Mathf.PI * 2f) * 0.5f + 0.5f);

        if (skyVolume.profile.TryGet<HDRISky>(out var hdriSky))
        {
            hdriSky.rotation.overrideState = true;
            hdriSky.exposure.overrideState = true;

            hdriSky.rotation.value = skyRotation;
            hdriSky.exposure.value = skyExposure;

            HDRenderPipeline hdPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
            hdPipeline?.RequestSkyEnvironmentUpdate();
        }

        // 🌅 Sun movement
        float sunAngle = Mathf.Lerp(-90f, 90f, Mathf.Sin(normalizedTime * Mathf.PI));
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);

        // 🌞 Intensity fades in/out
        directionalLight.intensity = Mathf.Clamp01(Mathf.Sin(normalizedTime * Mathf.PI * 2f)) * 1.5f;

        // 🎨 Optional: Color warms at sunrise/sunset
        directionalLight.color = Color.Lerp(new Color(1f, 0.6f, 0.4f), Color.white, Mathf.Clamp01(Mathf.Sin(normalizedTime * Mathf.PI * 2f)));
    }
}

