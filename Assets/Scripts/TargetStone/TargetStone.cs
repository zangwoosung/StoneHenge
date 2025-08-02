using System;
using System.Collections;
using TMPro;
using UnityEngine;
public enum StoneType
{
    Low,
    Middle,
    High
}
public class TargetStone : MonoBehaviour
{
    public static event Action<Transform, Transform> OnHitByProjectile;
    public static event Action<StoneType> OnKnockDownEvent;
    public static event Action<Vector3> OnKnockDownToAnimalEvent;
    public static event Action<Transform, float>   OnHitDistanceEvent; // EffectManager
    public static event Action<Vector3> OnHitContactEvent;  // 
    public static event Action  OnDisappearEvent;  // 

   

    public StoneType stoneType;
    public Renderer objRenderer;
    public float offset = 30f;
   
    float fadeDuration = 2f;
    Color originalColor;
    public bool isHit = false;
    MeshCollider meshCollider;

   
    private void OnEnable()
    {
        RaycastAtHeight.OnNoStoneStandingEvent += OnNoStoneStandingEvent;
        meshCollider = GetComponent<MeshCollider>();
       
        Vector3 highestPoint = gameObject.GetComponent<Collider>().bounds.max;
      
    }
    private void OnDestroy()
    {
        Debug.Log("Ondestory");
     
        RaycastAtHeight.OnNoStoneStandingEvent -= OnNoStoneStandingEvent;
    }
   

    private void OnNoStoneStandingEvent()
    {
        OnKnockDownToAnimalEvent?.Invoke(transform.position);
        StartCoroutine(FadeOutObject());
    }

    private void Start()
    {
        originalColor = objRenderer.material.color;
       

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            OnHitByProjectile?.Invoke(transform, collision.transform);
            isHit = true;
            
            foreach (ContactPoint hitcontact in collision.contacts)
            {
                VisualizeContact(hitcontact.point);
               
            }

            ContactPoint contact = collision.contacts[0];
            Vector3 contactPoint = contact.point;

            if (meshCollider != null)
            {
                float edgeDistance = EdgeDistanceCalculator.GetDistanceToNearestEdge(meshCollider, contactPoint);
                Debug.Log("Distance to nearest edge: " + edgeDistance);
                OnHitDistanceEvent.Invoke(transform, edgeDistance);
            }

        }
    }


    void VisualizeContact(Vector3 point)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = point;
        marker.transform.localScale = Vector3.one * 0.1f;
        marker.GetComponent<Renderer>().material.color = Color.red;
        Destroy(marker, 2f); 
    }
        
   

    IEnumerator FadeOutObject()
    {
        float elapsed = 0f;
        while (elapsed < 2)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsed / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            objRenderer.material.color = newColor;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Color finalColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        objRenderer.material.color = finalColor;
        yield return new WaitForSeconds(0.5f);
        OnKnockDownEvent?.Invoke(stoneType);
        Destroy(gameObject, 0.2f);
    }


}
