using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraLookSequence : MonoBehaviour
{
    [SerializeField] WorldSpaceNameTag nameTag;
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] Transform[] targets;
    CancellationTokenSource cts = new CancellationTokenSource();
    Vector3 originPos;
    Quaternion rotation;
        
    bool isRunning = true;
    public void Initialize()
    {
        CancellationToken token = cts.Token;
        isRunning = true;
        CreateBillBoard();
        StartCameraSequence(token);
    }



    private void CreateBillBoard()
    {
        foreach (Transform target in targets)
        {
            nameTag.CreateDisplay(target);
        }
    }

    async void StartCameraSequence(CancellationToken token)
    {
        while (isRunning)
        {
            foreach (Transform target in targets)
            {
                try
                {

                    token.ThrowIfCancellationRequested();

                }
                catch (Exception)
                {
                    Debug.Log("StartCameraSequence in catch");
                    cameraFollow.Restore();
                    break;
                }
                await LookAtTarget(target, token);
                await Task.Delay(1000); // Wait 1 second
            }
        }
    }

    async Task LookAtTarget(Transform target, CancellationToken token)
    {
        float elapsed = 0f;
        float duration = 1.5f;
        float smoothSpeed = 6;
        Vector3 offset = new Vector3(0, 5, -10);
        Vector3 targetCamPos = target.position + offset;

        while (elapsed < duration)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                Debug.Log("token LookAtTarget");
            }
            catch (Exception)
            {
                Debug.Log("LookAtTarget in catch");
                cameraFollow.Restore();
                break;
            }
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothSpeed * Time.deltaTime);
            transform.LookAt(target);
            await Task.Yield();
        }
    }
    public void StopCameraWork()
    {
        isRunning = false;
        cts.Cancel();
        Debug.Log("cancel");
    }
    void OnDestroy()
    {
        // Cancel if the object is destroyed
        cts?.Cancel();
        cts?.Dispose();
    }
}

