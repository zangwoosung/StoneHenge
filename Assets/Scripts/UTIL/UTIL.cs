using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UTIL : MonoBehaviour
{

    




    private static readonly System.Random random = new System.Random();

    public static Color GetSoftRandomColor()
    {
        // Generate hue between 0 and 360
        float hue = (float)random.NextDouble();

        // Keep saturation low for softness (e.g., 0.3 to 0.6)
        float saturation = 0.3f + (float)random.NextDouble() * 0.3f;

        // Keep brightness high (e.g., 0.8 to 1.0)
        float value = 0.8f + (float)random.NextDouble() * 0.2f;

        // Convert HSV to RGB
        Color color = Color.HSVToRGB(hue, saturation, value);
        return color;
    }

    public static void ChangeBackGroundColor(VisualElement root, float t)
    {
     
        float r = Mathf.Abs(Mathf.Sin(t * 0.5f)); // Red channel
        float g = Mathf.Abs(Mathf.Cos(t * 0.3f)); // Green channel
        float b = Mathf.Abs(Mathf.Tan(t * 0.1f)); // Blue channel (clamped below)

        b = Mathf.Clamp01(b); // Tan can spike, so clamp to [0,1]

        root.style.backgroundColor = new Color(r, g, b);

    }
    public static IEnumerator ChangeColorCor(VisualElement root)
    {
        float t=Time.deltaTime;
        while (IsRunning)
        {
            yield return null;// new WaitForSeconds(0.2f);
            t += Time.deltaTime;
            // Use trigonometric functions to create smooth oscillations
            float r = Mathf.Abs(Mathf.Sin(t * 0.5f)); // Red channel
            float g = Mathf.Abs(Mathf.Cos(t * 0.3f)); // Green channel
            float b = Mathf.Abs(Mathf.Tan(t * 0.1f)); // Blue channel (clamped below)
            Debug.Log(t);
            b = Mathf.Clamp01(b); // Tan can spike, so clamp to [0,1]

            root.style.backgroundColor = new Color(r, g, b);
            if (t > 50) t = 0;

        }

    }



    public static bool IsRunning=true;
    public static IEnumerator SineWaveAlpha(VisualElement bodyBox, float duration, float frequency = 1f, float minAlpha = 0.2f, float maxAlpha = 1f)
    {
        float elapsed = 0f;
        Color baseColor = bodyBox.style.backgroundColor.value;

        while (IsRunning)
        {

            float t = Mathf.Sin(elapsed * frequency * 2f * Mathf.PI) * 0.5f + 0.5f; // Normalize to [0,1]
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

            bodyBox.style.backgroundColor = new StyleColor(new Color(baseColor.r, baseColor.g, baseColor.b, alpha));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }


    public static VisualElement Create(params string[] classnames)
    {
        return Create<VisualElement>(classnames);

    }

    public static T Create<T>(params string[] classname) where T : VisualElement, new()
    {
        var ele = new T();
        foreach (var item in classname)
        {
            ele.AddToClassList(item);
        }

        return ele;
    }
}
