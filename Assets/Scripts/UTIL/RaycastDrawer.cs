using System;
using UnityEngine;


public class RaycastDrawer : MonoBehaviour
{
    public  event Action OnRayCastHitAnimalEvent;
    public bool isHasHit=false;
   
    
    void Update()
    {
        if (isHasHit) return; 
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float maxDistance = 100f;

       
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {

            isHasHit = true;
            Debug.Log("동물 감지");
            Debug.DrawLine(origin, hit.point, Color.red); 
            Destroy(hit.transform.gameObject);
            OnRayCastHitAnimalEvent?.Invoke();
        }
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.green); 
        }
    }
}
