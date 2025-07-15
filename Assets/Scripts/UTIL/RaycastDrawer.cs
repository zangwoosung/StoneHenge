using UnityEngine;

public class RaycastDrawer : MonoBehaviour
{
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float maxDistance = 100f;

        // Perform the raycast
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            Debug.DrawLine(origin, hit.point, Color.red); // Draw to hit point
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.green); // Draw full ray
        }
    }
}
