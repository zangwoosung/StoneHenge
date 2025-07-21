using System;
using Unity.VisualScripting;
using UnityEngine;


public class RaycastAtHeight : MonoBehaviour
{
 
    public event Action OnStoneHasFallenEvent;
    public float maxDistance = 100f;
    Transform target;
    Vector3 origin;
    Vector3 direction;
    Vector3 TargetHeight;
    bool isLocked = false;
    private Vector3 startPos;
    Vector3 GetTargetHeight(Renderer ren)
    {
        Renderer renderer = ren;    

        Bounds bounds = renderer.bounds;      
        Vector3 origin = new Vector3(
            bounds.center.x,
            bounds.min.y + bounds.size.y * (4f / 5f),
            bounds.center.z
        );
        myHeight = origin.y;
        return origin;
    }

    float myHeight = 0;

    public void Init(GameObject obj)
    {
        startPos = transform.position;

        target = obj.transform;

        TargetHeight= GetTargetHeight(obj.GetComponent<Renderer>());
        origin = transform.position;
        origin.y = TargetHeight.y;
        direction = transform.forward;// (TargetHeight - origin).normalized;

        //Vector3 direction = transform.forward;
        isLocked = true;
    }

    float lapTime = 0;
    float speed = 5;
    float distance = 10;
    void Update()
    {
        if (!isLocked) return;

        //float offset = Mathf.PingPong(Time.time * speed, distance);
        //transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);



        //origin = transform.position;
        //origin.y = TargetHeight.y;

        ////direction = (TargetHeight - origin).normalized;
        direction = transform.forward;// 

        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            lapTime += Time.deltaTime;
            if(lapTime > 2)
            {
                //has fallen down. 
                OnStoneHasFallenEvent?.Invoke();
                lapTime = 0;
            } 

            Debug.Log("Not Hit: ");
        }

        Debug.DrawRay(origin, direction * maxDistance, Color.red);
    }
}
