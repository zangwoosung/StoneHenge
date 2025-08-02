using System;
using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target = null;      
    public Vector3 offset = new Vector3(1, 0, 0); 
    public float smoothSpeed = 5f;
    Vector3 originPos;
    Quaternion rotation;   
    private void OnEnable()
    {
        originPos = transform.position;
        rotation = transform.rotation;       
    }

   

    public void SetTarget(Transform target)
    {
        this.target = target;
        smoothSpeed = 5f;
        transform.LookAt(target);       
    }
   

   
    void LateUpdate()
    {
        if (target==null) return;
        Vector3 desiredPosition;
        desiredPosition = target.position + offset;        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt (target);
    }

    public void Restore()
    {
        Debug.Log("Restore");
        target = null;
        transform.position = originPos;
        transform.rotation = rotation;
    }
}

