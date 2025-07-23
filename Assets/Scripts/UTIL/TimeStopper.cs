using UnityEngine;
using System.Collections;

public class TimeStopper : MonoBehaviour
{
    public float stopDuration = 2f;

    public void StopTimeTemporarily()
    {
        StartCoroutine(StopTimeCoroutine());
    }

    private IEnumerator StopTimeCoroutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(stopDuration); // unaffected by timeScale
        Time.timeScale = 1f;
    }
}

