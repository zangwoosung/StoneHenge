using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowTimeScale = 0.2f;
    public float slowDuration = 2f;

    private float originalTimeScale;
    private float originalFixedDeltaTime;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void OnReset()
    {
        StopAllCoroutines();
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
    public void TriggerSlowMotion()
    {
        StartCoroutine(SlowMotionCoroutine());
    }

    private IEnumerator SlowMotionCoroutine()
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime * slowTimeScale;

        yield return new WaitForSecondsRealtime(slowDuration);

        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}

