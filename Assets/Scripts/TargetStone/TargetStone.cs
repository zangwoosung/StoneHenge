using System;
using System.Collections;
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
    public static event Action<Transform, float> OnHitDistanceEvent; // EffectManager
    public static event Action<Vector3> OnHitContactEvent;  // 
    public static event Action OnDisappearEvent;  // 
    public StoneType stoneType; 
  
    Renderer objRenderer;
    MeshCollider meshCollider;
    Color originalColor;
    float lapTime = 0;
    float fadeDuration = 2f;
    bool isHasFallen = false;


    private void OnEnable()
    {        
        meshCollider = GetComponent<MeshCollider>();
        objRenderer = GetComponent<Renderer>();
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

            foreach (ContactPoint hitcontact in collision.contacts)
            {
                VisualizeContact(hitcontact.point);
            }

            ContactPoint contact = collision.contacts[0];
            Vector3 contactPoint = contact.point;

            if (meshCollider != null)
            {
                float edgeDistance = EdgeDistanceCalculator.GetDistanceToNearestEdge(meshCollider, contactPoint);
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
    float rayDistance = 1.5f;
    void Update()
    {
        if (isHasFallen) return; 

        Vector3 origin = transform.position;
        origin.y += 1;
        Vector3 direction = -transform.up;
        // Create a LayerMask that excludes layer 6
        int layerToExclude = 6;
        int layerMask = ~(1 << layerToExclude);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, layerMask))
        {
           // Debug.Log("Hit object: " + hit.collider.name);
            Debug.DrawRay(origin, direction * rayDistance, Color.green);
            lapTime = 0;

        }
        else
        {
            lapTime += Time.deltaTime;            
            Debug.DrawRay(origin, direction * rayDistance, Color.red);
            if (lapTime > 2)
            {
                isHasFallen = true;
                OnKnockDownToAnimalEvent?.Invoke(transform.position);
               // Debug.Log("It has fallen");
                Debug.DrawRay(origin, direction * rayDistance, Color.red);
                StartCoroutine(FadeOutObject());
            }
        }
        
    }

}
