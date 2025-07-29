using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastDrawer : MonoBehaviour
{
    public static event Action OnRayCastHitZombiEvent;
    public bool isHasHit=false;
    void Update()
    {
       // if (isHasHit) return; 
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float maxDistance = 100f;

       
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            Debug.Log("hit name " + hit.transform.name);
            Debug.DrawLine(origin, hit.point, Color.red); 
            Destroy(hit.transform.gameObject);
            OnRayCastHitZombiEvent?.Invoke();
            //isHasHit = true;
        }
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.green); 
        }
    }
}
