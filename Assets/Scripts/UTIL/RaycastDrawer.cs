using System;
using UnityEngine;


public class RaycastDrawer : MonoBehaviour
{
    public  event Action OnRayCastHitAnimalEvent;
    public bool isHasHit=false;
    public AnimalController animalController;

    
    void Update()
    {
        if (isHasHit) return; 
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float maxDistance = 100f;

       
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            Debug.Log("hit name " + hit.transform.name);
            Debug.DrawLine(origin, hit.point, Color.red); 
            Destroy(hit.transform.gameObject);
            animalController.RemoveAllAnimals();
            OnRayCastHitAnimalEvent?.Invoke();
            isHasHit = true;
        }
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.green); 
        }
    }
}
