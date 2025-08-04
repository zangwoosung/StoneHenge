using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraLookSequence : MonoBehaviour
{
    [SerializeField] WorldSpaceNameTag nameTag;
    [SerializeField] Transform[] targets;
    CancellationTokenSource cts = new CancellationTokenSource();

    public bool isRunning = true;
    private async void Start()
    {
        CancellationToken token = cts.Token;

        isRunning = true;
        CreateBillBoard();
        await StartSequence(token);
    }

    public void StopCameraWork()
    {
        cts.Cancel();
        //isRunning

    }

    private void CreateBillBoard()
    {
        foreach (Transform target in targets)
        {
            nameTag.CreateDisplay(target);
        }
    }

    async Task StartSequence(CancellationToken token)
    {
        while (isRunning) // Loop forever; remove if you want it to run once
        {
            foreach (Transform target in targets)
            {
                token.ThrowIfCancellationRequested();
                await LookAtTarget(target, token);
                await Task.Delay(1000); // Wait 1 second
            }
        }
        await Task.Delay(2000);
    }

    async Task LookAtTarget(Transform target, CancellationToken token)
    {
        float elapsed = 0f;
        float duration = 1.5f;
        float smoothSpeed = 6;
        Vector3 offset = new Vector3(0, 5, -10);
        Vector3 targetCamPos = target.position + offset;
        transform.LookAt(target);

        while (elapsed < duration)
        {
            token.ThrowIfCancellationRequested();
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothSpeed * Time.deltaTime);
            transform.LookAt(target);
            await Task.Yield();
        }
    }
}

