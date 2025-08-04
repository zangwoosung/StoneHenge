using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CameraLookSequence : MonoBehaviour
{
    [SerializeField] WorldSpaceNameTag worldSpaceNameTag;
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] Transform[] targets;

    //TODO 중요 
    CancellationTokenSource cts = new CancellationTokenSource();
   
    Vector3 originPos;
    Quaternion rotation;
        
    bool isRunning = true;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        CancellationToken token = cts.Token;
        isRunning = true;
        CreateBillBoard();
        StartCameraSequence(token);
    }


    void RemoveBillBoard()
    {

    }
    private void CreateBillBoard()
    {
        foreach (Transform target in targets)
        {
            worldSpaceNameTag.CreateDisplay(target);
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
               
            }
            catch (Exception)
            {
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
    }
    //TODO 중요. 반드시 제거 하기. 
    void OnDestroy()
    {       
        cts?.Cancel();
        cts?.Dispose();
    }
}

