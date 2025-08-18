using System;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DayAndNightCycle : MonoBehaviour
{
    public float currentTime = 0f; // Current time in the cycle
    public float timeSpeed = 1f;

    public Light sunLight; // Reference to the directional light representing the sun
    public float sunPosition=1f;
    public float sungIntencity;

    public AnimationCurve sunIntensityCurve; // Curve to control the intensity of the sun light over time;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLight();
    }
    private void OnValidate()
    {
       UpdateLight();
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * timeSpeed;

        if (currentTime >= 24f)
        {
            currentTime = 0f; // Reset the cycle after 24 hours
        }
        UpdateTimeText();
        UpdateLight();

    }

    private void UpdateLight()
    {
        float sunrotation = (currentTime / 24f) * 360f; // Convert time to degrees (0-360)  
        sunLight.transform.rotation = Quaternion.Euler(sunrotation - 90f, sunPosition, 0f); // Rotate the sun light      

        float normalizedTime = currentTime / 24f; // Normalize time to a 0-1 range
        float intensity = sunIntensityCurve.Evaluate(normalizedTime); // Evaluate the intensity curve
        sunLight.intensity = intensity; // Set the light intensity
        
        
        HDAdditionalLightData data = sunLight.GetComponent<HDAdditionalLightData>();

        if(data != null)
        {
            sunLight.intensity = intensity * sungIntencity; // Set the light dimmer value
        }
       
    }

    private void UpdateTimeText()
    {
        Debug.Log($"Current Time: {currentTime:F2} hours"); 
    }
}
