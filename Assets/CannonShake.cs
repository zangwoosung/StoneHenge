using System.Collections;
using UnityEngine;

public class CannonShake : MonoBehaviour
{
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.1f;

    public void Fire()
    {
        // Your projectile firing logic here
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offset = Random.Range(-shakeAmount, shakeAmount);
            transform.localPosition = originalPosition + new Vector3(offset, offset, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
